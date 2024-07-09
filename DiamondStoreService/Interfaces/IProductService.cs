using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IProductService
    {
        //Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId);
        Task<List<DiamondType>> GetAllDiamondTypes();
    }
}
