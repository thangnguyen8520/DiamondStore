using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IDiamondRepository
    {
        Task<IEnumerable<Diamond>> GetAllAsync();
        Task<Diamond> GetDiamondByIdAsync(int id);
        Task AddDiamondAsync(Diamond diamond);

        Task UpdateDiamondAsync(Diamond diamond);

        Task DeleteDiamondAsync(int id);
        Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId, string color, string clarity, string cut, double? minPrice, double? maxPrice, double? minDiameter, double? maxDiameter, double? minWeight, double? maxWeight);
        Task<List<DiamondType>> GetAllDiamondTypes();
        Task<List<DiamondClarity>> GetAllDiamondClarities(); 
        Task<List<DiamondColor>> GetAllDiamondColors();
        Task<List<DiamondCut>> GetAllDiamondCuts();
        Task<Diamond> GetById(int id, string includeProperties = "");
        Task<List<Diamond>> GetRelatedDiamonds(int? diamondTypeId, int excludeDiamondId);

    }
}
