using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class DiamondService : IDiamondService
    {
        private readonly IDiamondRepository _diamondRepository;

        public DiamondService(IDiamondRepository diamondRepository)
        {
            _diamondRepository = diamondRepository;
        }

        public async Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId, string color, string clarity, string cut, double? minPrice, double? maxPrice, double? minDiameter, double? maxDiameter, double? minWeight, double? maxWeight)
        {
            return await _diamondRepository.GetDiamonds(pageIndex, pageSize, sortOption, categoryId, color, clarity, cut, minPrice, maxPrice, minDiameter, maxDiameter, minWeight, maxWeight);
        }

        public async Task<List<DiamondType>> GetAllDiamondTypes()
        {
            return await _diamondRepository.GetAllDiamondTypes();
        }

        public async Task<List<DiamondClarity>> GetAllDiamondClarities()
        {
            return await _diamondRepository.GetAllDiamondClarities();
        }

        public async Task<List<DiamondColor>> GetAllDiamondColors()
        {
            return await _diamondRepository.GetAllDiamondColors();
        }

        public async Task<List<DiamondCut>> GetAllDiamondCuts()
        {
            return await _diamondRepository.GetAllDiamondCuts();
        }

        public async Task<Diamond> GetById(int id, string includeProperties = "")
        {
            return await _diamondRepository.GetById(id, includeProperties);
        }

        public async Task<List<Diamond>> GetRelatedDiamonds(int? diamondTypeId, int excludeDiamondId)
        {
            return await _diamondRepository.GetRelatedDiamonds(diamondTypeId, excludeDiamondId);
        }
    }
}
