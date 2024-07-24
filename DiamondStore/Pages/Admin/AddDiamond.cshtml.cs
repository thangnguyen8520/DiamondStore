using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DiamondStore.Pages.Admin
{
    public class AddDiamondModel : PageModel
    {
        private readonly IDiamondService _diamondService;

        public AddDiamondModel(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        [BindProperty]
        public DiamondDTO Diamond { get; set; }
        public IList<DiamondColorDTO> Colors { get; private set; }
        public IList<DiamondClarityDTO> Clarities { get; private set; }
        public IList<DiamondCutDTO> Cuts { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }

            Colors = (await _diamondService.GetAllDiamondColors()).Select(c => new DiamondColorDTO
            {
                Id = c.DiamondColorId,
                Name = c.DiamondColorName
            }).ToList();

            Clarities = (await _diamondService.GetAllDiamondClarities()).Select(c => new DiamondClarityDTO
            {
                Id = c.DiamondClarityId,
                Name = c.DiamondClarityName
            }).ToList();

            Cuts = (await _diamondService.GetAllDiamondCuts()).Select(c => new DiamondCutDTO
            {
                Id = c.DiamondCutId,
                Name = c.DiamondCutName
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _diamondService.AddDiamondAsync(Diamond);
            return RedirectToPage("/Admin/AdminDiamond");
        }
    }
}
