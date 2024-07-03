using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public Pagination<Diamond> Diamonds { get; set; }
        public List<DiamondType> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1; // Start from page 1
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 9;
        [BindProperty(SupportsGet = true)]
        public string SortOption { get; set; } = "Date, New To Old";
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public async Task OnGetAsync()
        {
            // Adjust PageIndex to be 0-based for the service layer
            int adjustedPageIndex = PageIndex - 1;
            Diamonds = await _productService.GetDiamonds(adjustedPageIndex, PageSize, SortOption, CategoryId);

            // Ensure PageIndex is within valid range
            if (PageIndex > Diamonds.TotalPagesCount)
            {
                PageIndex = Diamonds.TotalPagesCount > 0 ? Diamonds.TotalPagesCount : 1;
                adjustedPageIndex = PageIndex - 1;
                Diamonds = await _productService.GetDiamonds(adjustedPageIndex, PageSize, SortOption, CategoryId);
            }

            Categories = await _productService.GetAllDiamondTypes();
        }
    }
}
