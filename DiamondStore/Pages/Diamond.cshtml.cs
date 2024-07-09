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
    public class DiamondModel : PageModel
    {
        private readonly IDiamondService _diamondService;

        public DiamondModel(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        public Pagination<Diamond> Pagination { get; set; }
        public IList<DiamondType> Categories { get; set; }
        public IList<DiamondColor> Colors { get; set; }
        public IList<DiamondClarity> Clarities { get; set; }
        public IList<DiamondCut> Cuts { get; set; }
        public List<SelectListItem> SortOptions { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 9;
        [BindProperty(SupportsGet = true)]
        public string SortOption { get; set; } = "Date, New To Old";
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Color { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Clarity { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Cut { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? MinPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? MaxPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? MinDiameter { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? MaxDiameter { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? MinWeight { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? MaxWeight { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PriceRange { get; set; }
        [BindProperty(SupportsGet = true)]
        public string DiameterRange { get; set; }
        [BindProperty(SupportsGet = true)]
        public string WeightRange { get; set; }

        public async Task OnGetAsync()
        {
            // Adjust PageIndex to be 0-based for the service layer
            int adjustedPageIndex = PageIndex - 1;
            Categories = await _diamondService.GetAllDiamondTypes();
            Colors = await _diamondService.GetAllDiamondColors();
            Clarities = await _diamondService.GetAllDiamondClarities();
            Cuts = await _diamondService.GetAllDiamondCuts();

            SortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "PriceLowToHigh", Text = "Price, Low To High" },
                new SelectListItem { Value = "PriceHighToLow", Text = "Price, High To Low" },
                new SelectListItem { Value = "AlphabeticalAZ", Text = "Alphabetically, A-Z" },
                new SelectListItem { Value = "AlphabeticalZA", Text = "Alphabetically, Z-A" },
                new SelectListItem { Value = "DateNewToOld", Text = "Date, New To Old" },
                new SelectListItem { Value = "DateOldToNew", Text = "Date, Old To New" }
            };

            if (!string.IsNullOrEmpty(PriceRange))
            {
                var ranges = PriceRange.Split('-');
                MinPrice = double.Parse(ranges[0]);
                MaxPrice = double.Parse(ranges[1]);
            }

            if (!string.IsNullOrEmpty(DiameterRange))
            {
                var ranges = DiameterRange.Split('-');
                MinDiameter = double.Parse(ranges[0]);
                MaxDiameter = double.Parse(ranges[1]);
            }

            if (!string.IsNullOrEmpty(WeightRange))
            {
                var ranges = WeightRange.Split('-');
                MinWeight = double.Parse(ranges[0]);
                MaxWeight = double.Parse(ranges[1]);
            }

            Pagination = await _diamondService.GetDiamonds(adjustedPageIndex, PageSize, SortOption, CategoryId, Color, Clarity, Cut, MinPrice, MaxPrice, MinDiameter, MaxDiameter, MinWeight, MaxWeight);

            // Ensure PageIndex is within valid range
            if (PageIndex > Pagination.TotalPagesCount)
            {
                PageIndex = Pagination.TotalPagesCount > 0 ? Pagination.TotalPagesCount : 1;
                adjustedPageIndex = PageIndex - 1;
                Pagination = await _diamondService.GetDiamonds(adjustedPageIndex, PageSize, SortOption, CategoryId, Color, Clarity, Cut, MinPrice, MaxPrice, MinDiameter, MaxDiameter, MinWeight, MaxWeight);
            }

            // Set the selected sort option in the drop-down
            foreach (var option in SortOptions)
            {
                if (option.Value == SortOption)
                {
                    option.Selected = true;
                    break;
                }
            }
        }
    }
}
