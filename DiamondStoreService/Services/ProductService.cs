using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId)
        {
            return await _productRepository.GetDiamonds(pageIndex, pageSize, sortOption, categoryId);
        }

        public async Task<List<DiamondType>> GetAllDiamondTypes()
        {
            return await _productRepository.GetAllDiamondTypes();
        }
    }
}
