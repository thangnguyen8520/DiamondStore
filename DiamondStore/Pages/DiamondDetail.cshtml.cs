using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class DiamondDetailModel : PageModel
    {
        private readonly IDiamondRepository _diamondRepository;

        public DiamondDetailModel(IDiamondRepository diamondRepository)
        {
            _diamondRepository = diamondRepository;
        }

        public Diamond Diamond { get; set; }
        public List<Diamond> RelatedDiamonds { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Diamond = await _diamondRepository.GetById(id, "DiamondColor,DiamondClarity,DiamondCut,Image");

            if (Diamond == null)
            {
                return NotFound();
            }

            RelatedDiamonds = await _diamondRepository.GetRelatedDiamonds(Diamond.DiamondTypeId, id);

            return Page();
        }
    }
}
