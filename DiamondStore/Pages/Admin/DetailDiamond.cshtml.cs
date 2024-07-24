using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class DetailDiamondModel : PageModel
    {
        private readonly IDiamondService _diamondService;

        public DetailDiamondModel(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        public DiamondDTO Diamond { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }
            Diamond = await _diamondService.GetDiamondByIdAsync(id);
            return Page();


        }
    }
}
