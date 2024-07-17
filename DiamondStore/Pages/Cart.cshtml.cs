using DiamondBusinessObject.Models;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IPromotionService _promotionService;

        public CartModel(ICartService cartService, IPromotionService promotionService)
        {
            _cartService = cartService;
            _promotionService = promotionService;
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
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int cartId)
        {
            await _cartService.DeleteCartItem(cartId);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int cartId, int quantity)
        {
            var cartItem = await _cartService.GetCartItem(cartId);
            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    await _cartService.DeleteCartItem(cartId);
                }
                else
                {
                    cartItem.Quantity = quantity;
                    await _cartService.UpdateCartItem(cartItem);
                }
            }
            return RedirectToPage();
        }
    }
}
