using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IJewelryRepository : IGenericRepository<Jewelry>
    {
        float CalculateTotalPrice(Jewelry jewelry);
        Task<IEnumerable<Jewelry>> GetAllAsync();
        Task<Pagination<Jewelry>> GetPaginated(int pageIndex, int pageSize, string sortOption, int? typeId, string material, int? sizeId, string priceRange);
        Task<Jewelry> GetJewelryWithDetails(int jewelryId);
        Task<List<Jewelry>> GetRelatedJewelries(int typeId, int excludeId);
        Task<Jewelry> GetJewelryByIdAsync(int id);
        Task AddJewelryAsync(Jewelry jewelry);

        Task UpdateJewelryAsync(Jewelry jewelry);

        Task DeleteJewelryAsync(int id);
    }
}
