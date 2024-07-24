using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
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

        public async Task<IList<DiamondDTO>> GetAllDiamondsAsync()
        {
            var diamonds = await _diamondRepository.GetAllAsync();
            return diamonds.Select(d => new DiamondDTO
            {
                DiamondId = d.DiamondId,
                DiamondName = d.DiamondName,
                DiamondPrice = d.DiamondPrice,
                DiamondWeight = d.DiamondWeight,
                DiamondColorName = d.DiamondColor?.DiamondColorName,
                DiamondClarityName = d.DiamondClarity?.DiamondClarityName,
                DiamondCutName = d.DiamondCut?.DiamondCutName,
                DiamondTypeName = d.DiamondType?.DiamondTypeName,
                DiamondDiameter = d.DiamondDiameter,
                DiamondCertificate = d.DiamondCertificate,
                DiamondInventory = d.DiamondInventory,
                CreateDate = d.CreateDate
            }).ToList();
        }

        public async Task<DiamondDTO> GetDiamondByIdAsync(int id)
        {
            var diamond = await _diamondRepository.GetDiamondByIdAsync(id);
            if (diamond != null)
            {
                return new DiamondDTO
                {
                    DiamondId = diamond.DiamondId,
                    DiamondName = diamond.DiamondName,
                    DiamondPrice = diamond.DiamondPrice,
                    DiamondWeight = diamond.DiamondWeight,
                    DiamondColorName = diamond.DiamondColor?.DiamondColorName,
                    DiamondClarityName = diamond.DiamondClarity?.DiamondClarityName,
                    DiamondCutName = diamond.DiamondCut?.DiamondCutName,
                    DiamondTypeName = diamond.DiamondType?.DiamondTypeName,
                    DiamondDiameter = diamond.DiamondDiameter,
                    DiamondCertificate = diamond.DiamondCertificate,
                    DiamondInventory = diamond.DiamondInventory,
                    CreateDate = diamond.CreateDate
                };
            }
            return null;
        }

        public async Task AddDiamondAsync(DiamondDTO diamondDto)
        {
            var diamond = new Diamond
            {
                DiamondName = diamondDto.DiamondName,
                DiamondPrice = diamondDto.DiamondPrice,
                DiamondWeight = diamondDto.DiamondWeight,
                DiamondColorId = diamondDto.DiamondColorId,
                DiamondClarityId = diamondDto.DiamondClarityId,
                DiamondCutId = diamondDto.DiamondCutId,
                DiamondTypeId = diamondDto.DiamondTypeId,
                DiamondDiameter = diamondDto.DiamondDiameter,
                DiamondCertificate = diamondDto.DiamondCertificate,
                DiamondInventory = diamondDto.DiamondInventory,
                CreateDate = diamondDto.CreateDate
            };
            await _diamondRepository.AddDiamondAsync(diamond);
        }

        public async Task UpdateDiamondAsync(DiamondDTO diamondDto)
        {
            var diamond = await _diamondRepository.GetDiamondByIdAsync(diamondDto.DiamondId);
            if (diamond != null)
            {
                diamond.DiamondName = diamondDto.DiamondName;
                diamond.DiamondPrice = diamondDto.DiamondPrice;
                diamond.DiamondWeight = diamondDto.DiamondWeight;
                diamond.DiamondColorId = diamondDto.DiamondColorId;
                diamond.DiamondClarityId = diamondDto.DiamondClarityId;
                diamond.DiamondCutId = diamondDto.DiamondCutId;
                diamond.DiamondTypeId = diamondDto.DiamondTypeId;
                diamond.DiamondDiameter = diamondDto.DiamondDiameter;
                diamond.DiamondCertificate = diamondDto.DiamondCertificate;
                diamond.DiamondInventory = diamondDto.DiamondInventory;
                diamond.CreateDate = diamondDto.CreateDate;

                await _diamondRepository.UpdateDiamondAsync(diamond);
            }
        }

        public async Task DeleteDiamondAsync(int id)
        {
            await _diamondRepository.DeleteDiamondAsync(id);
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
