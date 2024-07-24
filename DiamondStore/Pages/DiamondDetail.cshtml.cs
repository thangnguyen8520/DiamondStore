using DiamondBusinessObject.Models;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class DiamondDetailModel : PageModel
    {
        private readonly IDiamondService _diamondService;
        private readonly ICartService _cartService;

        public DiamondDetailModel(IDiamondService diamondService, ICartService cartService)
        {
            _diamondService = diamondService;
            _cartService = cartService;
        }

        public Diamond Diamond { get; set; }
        public List<Diamond> RelatedDiamonds { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Auth/Login");
            }

            Diamond = await _diamondService.GetById(id, "DiamondColor,DiamondClarity,DiamondCut,Image");

            if (Diamond == null)
            {
                return NotFound();
            }

            RelatedDiamonds = await _diamondService.GetRelatedDiamonds(Diamond.DiamondTypeId, id);

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("Auth/Login");
            }

            try
            {
                var activeCart = await _cartService.GetActiveCartByUserId(userId);

                if (activeCart == null)
                {
                    activeCart = new Cart { UserId = userId, Status = true };
                    await _cartService.AddToCart(activeCart);
                }

                var existingCartItem = activeCart.CartDiamonds.FirstOrDefault(cd => cd.DiamondId == id);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += 1;
                    existingCartItem.AddDate = DateTime.Now;
                }
                else
                {
                    activeCart.CartDiamonds.Add(new CartDiamond { DiamondId = id, Quantity = 1, AddDate = DateTime.Now });
                }
                activeCart.CreateDate = DateTime.Now;
                await _cartService.UpdateCartItem(activeCart);

                TempData["SuccessMessage"] = "Diamond added to cart successfully!";
                return RedirectToPage(new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "An error occurred while adding the diamond to the cart.";
                return RedirectToPage(new { id });
            }
        }
    }
}
