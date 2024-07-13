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

        public JewelryDetailModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public Jewelry Jewelry { get; set; }
        public IList<JewelrySize> Sizes { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Jewelry = await _jewelryService.GetJewelryWithDetails(id);
            Sizes = await _jewelryService.GetAllJewelrySizes();

            if (Jewelry == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
