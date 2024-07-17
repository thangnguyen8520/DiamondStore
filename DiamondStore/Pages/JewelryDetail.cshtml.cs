using DiamondBusinessObject.Models;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class JewelryDetailModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly ICartService _cartService;

        public JewelryDetailModel(IJewelryService jewelryService, ICartService cartService)
        {
            _jewelryService = jewelryService;
            _cartService = cartService;
        }

        public Jewelry Jewelry { get; set; }
        public IList<JewelrySize> Sizes { get; set; }
        public List<Jewelry> RelatedJewelry { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Jewelry = await _jewelryService.GetJewelryWithDetails(id);
            Sizes = await _jewelryService.GetAllJewelrySizes();

            if (Jewelry == null)
            {
                return NotFound();
            }

            RelatedJewelry = await _jewelryService.GetRelatedJewelries(Jewelry.JewelryId, id);

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id, int sizeId)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("Auth/Login");
            }

            if (sizeId == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select a size.");
                Jewelry = await _jewelryService.GetJewelryWithDetails(id);
                Sizes = await _jewelryService.GetAllJewelrySizes();
                RelatedJewelry = await _jewelryService.GetRelatedJewelries(Jewelry.JewelryId, id);
                return Page();
            }

            var existingCartItem = await _cartService.GetCartItemByDetails(userId, id, null, sizeId);
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
                    JewelryId = id,
                    JewelrySizeId = sizeId,
                    Quantity = 1,
                    CreateDate = DateTime.Now
                };
                await _cartService.AddToCart(cartItem);
            }

            TempData["SuccessMessage"] = "Jewelry added to cart successfully!";
            return RedirectToPage(new { id });
        }

    }
}
