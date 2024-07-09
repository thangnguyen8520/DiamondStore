using DiamondBusinessObject.Models;
using DiamondDAO;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO;

        public ProductRepository(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        //public async Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId)
        //{
        //    var query = (await _productDAO.GetAllDiamonds()).AsQueryable();

        //    if (categoryId.HasValue)
        //    {
        //        query = query.Where(d => d.DiamondTypeId == categoryId.Value);
        //    }

        //    query = sortOption switch
        //    {
        //        "Price, Low To High" => query.OrderBy(d => d.DiamondPrice.PricePerCarat),
        //        "Price, High To Low" => query.OrderByDescending(d => d.DiamondPrice.PricePerCarat),
        //        "Alphabetically, A-Z" => query.OrderBy(d => d.DiamondName),
        //        "Alphabetically, Z-A" => query.OrderByDescending(d => d.DiamondName),
        //        "Date, New To Old" => query.OrderByDescending(d => d.DiamondPrice.UpdateDate),
        //        _ => query.OrderBy(d => d.DiamondName),
        //    };

        //    var totalItems = query.Count();
        //    var items = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

        //    return new Pagination<Diamond>
        //    {
        //        Items = items,
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        TotalItemsCount = totalItems
        //    };
        //}

        public async Task<List<DiamondType>> GetAllDiamondTypes()
        {
            return await _productDAO.GetAllDiamondTypes();
        }
    }
}
