using System.Collections.Generic;
using System.Threading.Tasks;
using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiamondStore.Pages
{
    public class JewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;

        public JewelryModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public Pagination<Jewelry> Pagination { get; set; }
        public IList<JewelryType> Types { get; set; }
        public IList<JewelryMaterial> Materials { get; set; }
        public string SortOption { get; set; } = "Date, New To Old";
        public List<SelectListItem> SortOptions { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? TypeId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Material { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PriceRange { get; set; }

        public async Task OnGetAsync(int pageIndex = 1, int pageSize = 9, string sortOption = null)
        {
            Types = await _jewelryService.GetAllJewelryTypes();
            Materials = await _jewelryService.GetAllJewelryMaterials();

            SortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "DateNewToOld", Text = "Date, New To Old" },
                new SelectListItem { Value = "DateOldToNew", Text = "Date, Old To New" },
                new SelectListItem { Value = "PriceLowToHigh", Text = "Price, Low To High" },
                new SelectListItem { Value = "PriceHighToLow", Text = "Price, High To Low" },
                new SelectListItem { Value = "AlphabeticalAZ", Text = "Alphabetically, A-Z" },
                new SelectListItem { Value = "AlphabeticalZA", Text = "Alphabetically, Z-A" }
            };

            SortOption = sortOption;

            double? minPrice = null;
            double? maxPrice = null;

            if (!string.IsNullOrEmpty(PriceRange))
            {
                var ranges = PriceRange.Split('-');
                minPrice = double.Parse(ranges[0]);
                maxPrice = double.Parse(ranges[1]);
            }

            // Ensure pageIndex is positive
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            Pagination = await _jewelryService.GetJewelries(pageIndex, pageSize, sortOption, TypeId, Material, null, PriceRange);

            // Ensure the pageIndex is within the correct range
            if (pageIndex > Pagination.TotalPagesCount)
            {
                pageIndex = Pagination.TotalPagesCount;
                Pagination = await _jewelryService.GetJewelries(pageIndex, pageSize, sortOption, TypeId, Material, null, PriceRange);
            }
        }
    }
}
