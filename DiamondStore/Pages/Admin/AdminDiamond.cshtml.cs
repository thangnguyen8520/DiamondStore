using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class AdminDiamondModel : PageModel
    {
        private readonly IDiamondService _diamondService;

        public AdminDiamondModel(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        public IList<DiamondDTO> Diamonds { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }
            Diamonds = (await _diamondService.GetAllDiamondsAsync()).ToList();

            return Page();

        }
    }
}
