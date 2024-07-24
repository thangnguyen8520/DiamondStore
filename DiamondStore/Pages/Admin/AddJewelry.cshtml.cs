using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DiamondStore.Pages.Admin
{
    public class AddJewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IDiamondService _diamondService;

        public AddJewelryModel(
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

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }

            Diamonds = (await _diamondService.GetAllDiamondsAsync()).Select(d => new DiamondDTO
            {
                DiamondId = d.DiamondId,
                DiamondName = d.DiamondName,
                DiamondPrice = d.DiamondPrice,
                DiamondWeight = d.DiamondWeight,
                DiamondColorName = d.DiamondColorName,
                DiamondClarityName = d.DiamondClarityName,
                DiamondCutName = d.DiamondCutName,
                DiamondTypeName = d.DiamondTypeName,
                DiamondDiameter = d.DiamondDiameter,
                DiamondCertificate = d.DiamondCertificate,
                DiamondInventory = d.DiamondInventory,
                CreateDate = d.CreateDate,
                DiamondColorId = d.DiamondColorId,
                DiamondClarityId = d.DiamondClarityId,
                DiamondCutId = d.DiamondCutId,
                DiamondTypeId = d.DiamondTypeId
            }).ToList();

            Materials = (await _jewelryService.GetAllJewelryMaterials()).Select(m => new JewelryMaterialDTO
            {
                Id = m.JewelryMaterialId,
                Name = m.JewelryMaterialName
            }).ToList();

            Types = (await _jewelryService.GetAllJewelryTypes()).Select(t => new JewelryTypeDTO
            {
                Id = t.JewelryTypeId,
                Name = t.JewelryTypeName
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _jewelryService.AddJewelryAsync(Jewelry);
            return RedirectToPage("/Admin/AdminJewelry");
        }
    }
}
