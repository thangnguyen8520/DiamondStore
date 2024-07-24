using DiamondBusinessObject.Models;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DiamondStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IPromotionService _promotionService;
        private readonly ILogger<CartModel> _logger;

        public CartModel(ICartService cartService, IPromotionService promotionService, ILogger<CartModel> logger)
        {
            _cartService = cartService;
            _promotionService = promotionService;
            _logger = logger;
        }

        public List<Cart> CartItems { get; set; } = new List<Cart>();
        public List<UserPromotion> UserPromotions { get; set; } = new List<UserPromotion>();
        public List<CartPromotion> CartPromotions { get; set; } = new List<CartPromotion>();

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Auth/Login");
            }

            CartItems = await _cartService.GetCartItems(userId);
            UserPromotions = await _promotionService.GetUserPromotions(userId);
            CartPromotions = await _cartService.GetCartPromotions(userId);

            // Apply promotions to calculate the total price
            ApplyPromotions();

            return Page();
        }

        private void ApplyPromotions()
        {
            foreach (var cartItem in CartItems)
            {
                float totalDiscount = 0;
                foreach (var promotion in CartPromotions)
                {
                    var appliedPromotion = UserPromotions.FirstOrDefault(up => up.UserPromotionId == promotion.UserPromotionId);
                    if (appliedPromotion != null)
                    {
                        totalDiscount += (float)(cartItem.TotalPrice * appliedPromotion.Promotion.DiscountRate / 100.0);
                    }
                }
                cartItem.DisplayTotalPrice = cartItem.TotalPrice - totalDiscount;
            }
        }

        public async Task<IActionResult> OnPostApplyPromotionsAsync(List<int> selectedPromotions)
        {
            string userId = HttpContext.Session.GetString("UserId");
            var cart = await _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                return RedirectToPage();
            }

            var currentPromotions = await _cartService.GetCartPromotions(userId);
            var promotionsToAdd = selectedPromotions.Except(currentPromotions.Select(cp => cp.UserPromotionId)).ToList();
            var promotionsToRemove = currentPromotions.Where(cp => !selectedPromotions.Contains(cp.UserPromotionId)).ToList();

            foreach (var promotionId in promotionsToAdd)
            {
                await _cartService.AddCartPromotion(new CartPromotion { CartId = cart.CartId, UserPromotionId = promotionId });
            }

            foreach (var cartPromotion in promotionsToRemove)
            {
                await _cartService.RemoveCartPromotion(cartPromotion.CartPromotionId);
            }

            CartPromotions = await _cartService.GetCartPromotions(userId);
            CartItems = await _cartService.GetCartItems(userId);
            UserPromotions = await _promotionService.GetUserPromotions(userId);

            // Recalculate the total price with promotions
            ApplyPromotions();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int cartId, int itemId, string itemType)
        {
            _logger.LogInformation($"Removing item from cart. CartId: {cartId}, ItemId: {itemId}, ItemType: {itemType}");

            if (itemType == "Diamond")
            {
                await _cartService.DeleteCartDiamond(itemId);
            }
            else if (itemType == "Jewelry")
            {
                await _cartService.DeleteCartJewelry(itemId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int cartId, int itemId, int quantity, string itemType)
        {
            _logger.LogInformation($"Received values - CartId: {cartId}, ItemId: {itemId}, Quantity: {quantity}, ItemType: {itemType}");

            try
            {
                if (quantity <= 0)
                {
                    if (itemType == "Diamond")
                    {
                        _logger.LogInformation($"Deleting diamond from cart. ItemId: {itemId}");
                        await _cartService.DeleteCartDiamond(itemId);
                    }
                    else if (itemType == "Jewelry")
                    {
                        _logger.LogInformation($"Deleting jewelry from cart. ItemId: {itemId}");
                        await _cartService.DeleteCartJewelry(itemId);
                    }
                }
                else
                {
                    if (itemType == "Diamond")
                    {
                        var cartDiamond = await _cartService.GetCartDiamondById(itemId);
                        if (cartDiamond != null)
                        {
                            if (cartDiamond.Diamond == null)
                            {
                                ModelState.AddModelError(string.Empty, "Diamond details not found.");
                                return RedirectToPage();
                            }

                            if (quantity > cartDiamond.Diamond.DiamondInventory)
                            {
                                ModelState.AddModelError(string.Empty, $"Cannot update quantity. Only {cartDiamond.Diamond.DiamondInventory} items in stock.");
                                return RedirectToPage();
                            }

                            _logger.LogInformation($"Updating diamond quantity. ItemId: {itemId}, Quantity: {quantity}");
                            cartDiamond.Quantity = quantity;
                            await _cartService.UpdateCartDiamondQuantity(itemId, quantity);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Cart diamond not found.");
                            return RedirectToPage();
                        }
                    }
                    else if (itemType == "Jewelry")
                    {
                        var cartJewelry = await _cartService.GetCartJewelryById(itemId);
                        if (cartJewelry != null)
                        {
                            if (cartJewelry.Jewelry == null)
                            {
                                ModelState.AddModelError(string.Empty, "Jewelry details not found.");
                                return RedirectToPage();
                            }

                            if (quantity > cartJewelry.Jewelry.JewelryInventory)
                            {
                                ModelState.AddModelError(string.Empty, $"Cannot update quantity. Only {cartJewelry.Jewelry.JewelryInventory} items in stock.");
                                return RedirectToPage();
                            }

                            _logger.LogInformation($"Updating jewelry quantity. ItemId: {itemId}, Quantity: {quantity}");
                            cartJewelry.Quantity = quantity;
                            await _cartService.UpdateCartJewelryQuantity(itemId, quantity);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Cart jewelry not found.");
                            return RedirectToPage();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart item.");
                throw;
            }

            return RedirectToPage();
        }
    }
}
