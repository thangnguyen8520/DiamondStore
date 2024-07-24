using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
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

        public async Task OnGetAsync()
        {
            Diamonds = (await _diamondService.GetAllDiamondsAsync()).ToList();
        }
    }
}
