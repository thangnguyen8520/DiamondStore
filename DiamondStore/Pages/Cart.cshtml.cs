using DiamondBusinessObject.Models;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;

        public CartModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        public List<Cart> CartItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get the logged-in user's ID
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                // If user is not logged in, redirect to login page
                return RedirectToPage("/Auth/Login");
            }

            CartItems = await _cartService.GetCartItems(userId);
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
                if (quantity == 1)
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
