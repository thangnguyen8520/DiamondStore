using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class DetailJewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;

        public DetailJewelryModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public JewelryDTO Jewelry { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }
            Jewelry = await _jewelryService.GetJewelryByIdAsync(id);

            return Page();

        }
    }
}
