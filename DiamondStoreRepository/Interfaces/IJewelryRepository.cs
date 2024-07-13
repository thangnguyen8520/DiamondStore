using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IJewelryRepository : IGenericRepository<Jewelry>
    {
        Task<Pagination<Jewelry>> GetPaginated(int pageIndex, int pageSize, string sortOption, int? typeId, string material, int? sizeId, string priceRange);
        Task<Jewelry> GetJewelryWithDetails(int jewelryId);
        Task<List<Jewelry>> GetRelatedJewelries(int typeId, int excludeId);
    }
}
