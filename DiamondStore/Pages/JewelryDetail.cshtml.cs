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
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Auth/Login");
            }

            Jewelry = await _jewelryService.GetJewelryWithDetails(id);
            Sizes = await _jewelryService.GetAllJewelrySizes();

            if (Jewelry == null)
            {
                return NotFound();
            }

            RelatedJewelry = await _jewelryService.GetRelatedJewelries(Jewelry.JewelryTypeId, id);

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
                RelatedJewelry = await _jewelryService.GetRelatedJewelries(Jewelry.JewelryTypeId, id);
                return Page();
            }

            try
            {
                var activeCart = await _cartService.GetActiveCartByUserId(userId);

                if (activeCart == null)
                {
                    activeCart = new Cart { UserId = userId, Status = true };
                    await _cartService.AddToCart(activeCart);
                }

                var existingCartItem = activeCart.CartJewelries.FirstOrDefault(cj => cj.JewelryId == id && cj.JewelrySizeId == sizeId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += 1;
                    existingCartItem.AddDate = DateTime.Now;
                }
                else
                {
                    activeCart.CartJewelries.Add(new CartJewelry { JewelryId = id, JewelrySizeId = sizeId, Quantity = 1, AddDate = DateTime.Now });
                }
                activeCart.CreateDate = DateTime.Now;
                await _cartService.UpdateCartItem(activeCart);

                TempData["SuccessMessage"] = "Jewelry added to cart successfully!";
                return RedirectToPage(new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "An error occurred while adding the jewelry to the cart.";
                return RedirectToPage(new { id });
            }
        }
    }
}
