using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IJewelryService
    {
        Task<IList<JewelryDTO>> GetAllJewelriesAsync();
        Task<Pagination<Jewelry>> GetJewelries(int pageIndex, int pageSize, string sortOption, int? typeId, string material, int? sizeId, string priceRange);
        Task<IList<JewelryType>> GetAllJewelryTypes();
        Task<IList<JewelryMaterial>> GetAllJewelryMaterials();
        Task<IList<JewelrySize>> GetAllJewelrySizes();
        Task<Jewelry> GetJewelryWithDetails(int jewelryId);
        Task<List<Jewelry>> GetRelatedJewelries(int typeId, int excludeId);
        Task<bool> SizeExists(int sizeId);
    }
}
