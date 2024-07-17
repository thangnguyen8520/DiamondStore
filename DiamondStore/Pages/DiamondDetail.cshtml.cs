using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class DiamondDetailModel : PageModel
    {
        private readonly IDiamondRepository _diamondRepository;
        private readonly ICartService _cartService;

        public DiamondDetailModel(IDiamondRepository diamondRepository, ICartService cartService)
        {
            _diamondRepository = diamondRepository;
            _cartService = cartService;
        }

        public Diamond Diamond { get; set; }
        public List<Diamond> RelatedDiamonds { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Diamond = await _diamondRepository.GetById(id, "DiamondColor,DiamondClarity,DiamondCut,Image");

            if (Diamond == null)
            {
                return NotFound();
            }

            RelatedDiamonds = await _diamondRepository.GetRelatedDiamonds(Diamond.DiamondTypeId, id);

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            var existingCartItem = await _cartService.GetCartItemByDetails(userId, null, id, null);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                existingCartItem.CreateDate = DateTime.Now;
                await _cartService.UpdateCartItem(existingCartItem);
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = userId,
                    DiamondId = id,
                    Quantity = 1,
                    CreateDate = DateTime.Now
                };
                await _cartService.AddToCart(cartItem);
            }

            TempData["SuccessMessage"] = "Diamond added to cart successfully!";
            return RedirectToPage(new { id });
        }


    }

}
