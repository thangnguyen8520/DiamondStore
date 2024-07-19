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

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Auth/Login");
            }

            CartItems = await _cartService.GetCartItems(userId);
            UserPromotions = await _promotionService.GetUserPromotions(userId);

            foreach (var cartItem in CartItems)
            {
                foreach (var cartJewelry in cartItem.CartJewelries)
                {
                    float mainDiamondPrice = cartJewelry.Jewelry.Diamond?.DiamondPrice ?? 0;
                    float secondaryDiamondPrice = cartJewelry.Jewelry.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
                    cartJewelry.Jewelry.TotalPrice = 1.3f * (mainDiamondPrice + secondaryDiamondPrice + cartJewelry.Jewelry.JewelryPrice + cartJewelry.Jewelry.LaborCost);
                }
            }

            return Page();
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
                        _logger.LogInformation($"Updating diamond quantity. ItemId: {itemId}, Quantity: {quantity}");
                        await _cartService.UpdateCartDiamondQuantity(itemId, quantity);
                    }
                    else if (itemType == "Jewelry")
                    {
                        _logger.LogInformation($"Updating jewelry quantity. ItemId: {itemId}, Quantity: {quantity}");
                        await _cartService.UpdateCartJewelryQuantity(itemId, quantity);
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

        public async Task<IActionResult> OnPostApplyPromotionsAsync(List<int> selectedPromotions)
        {
            return RedirectToPage();
        }
    }
}
