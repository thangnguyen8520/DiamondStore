using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class EditJewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IDiamondService _diamondService;

        public EditJewelryModel(
            IJewelryService jewelryService,
            IDiamondService diamondService)
        {
            _jewelryService = jewelryService;
            _diamondService = diamondService;
        }

        [BindProperty]
        public JewelryDTO Jewelry { get; set; }
        public IList<DiamondDTO> Diamonds { get; private set; }
        public IList<JewelryMaterialDTO> Materials { get; private set; }
        public IList<JewelryTypeDTO> Types { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }
            Jewelry = await _jewelryService.GetJewelryByIdAsync(id);
            if (Jewelry == null)
            {
                return NotFound();
            }

            Diamonds = await _diamondService.GetAllDiamondsAsync();
            Materials = (IList<JewelryMaterialDTO>)await _jewelryService.GetAllJewelryMaterials();
            Types = (IList<JewelryTypeDTO>)await _jewelryService.GetAllJewelryTypes();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _jewelryService.UpdateJewelryAsync(Jewelry);
            return RedirectToPage("/Admin/AdminJewelry");
        }
    }
}
