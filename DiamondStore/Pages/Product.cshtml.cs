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
        public int PageIndex { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 9;
        [BindProperty(SupportsGet = true)]
        public string SortOption { get; set; } = "Price, Low To High";
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public async Task OnGetAsync()
        {
            Diamonds = await _productService.GetDiamonds(PageIndex, PageSize, SortOption, CategoryId);
            Categories = await _productService.GetCategories();
        }
    }
}
