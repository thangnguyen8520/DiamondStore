using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
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

        public async Task OnGetAsync()
        {
            Jewelries = (await _jewelryService.GetAllJewelriesAsync()).ToList();
        }
    }
}
