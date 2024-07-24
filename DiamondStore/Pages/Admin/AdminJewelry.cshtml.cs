using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class AdminJewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;

        public AdminJewelryModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public IList<JewelryDTO> Jewelries { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }
            Jewelries = (await _jewelryService.GetAllJewelriesAsync()).ToList();

            return Page();

        }
    }
}
