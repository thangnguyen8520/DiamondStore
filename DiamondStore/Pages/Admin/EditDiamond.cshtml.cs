using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class EditDiamondModel : PageModel
    {
        private readonly IDiamondService _diamondService;

        public EditDiamondModel(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        [BindProperty]
        public DiamondDTO Diamond { get; set; }
        public IList<DiamondColorDTO> Colors { get; private set; }
        public IList<DiamondClarityDTO> Clarities { get; private set; }
        public IList<DiamondCutDTO> Cuts { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }
            Diamond = await _diamondService.GetDiamondByIdAsync(id);
            if (Diamond == null)
            {
                return NotFound();
            }

            Colors = (IList<DiamondColorDTO>)await _diamondService.GetAllDiamondColors();
            Clarities = (IList<DiamondClarityDTO>)await _diamondService.GetAllDiamondClarities();
            Cuts = (IList<DiamondCutDTO>)await _diamondService.GetAllDiamondCuts();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _diamondService.UpdateDiamondAsync(Diamond);
            return RedirectToPage("/Admin/AdminDiamond");
        }
    }
}
