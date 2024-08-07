﻿using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IDiamondService
    {
        Task<IList<DiamondDTO>> GetAllDiamondsAsync();
        Task<DiamondDTO> GetDiamondByIdAsync(int id);
        Task DeleteDiamondAsync(int id);
        Task AddDiamondAsync(DiamondDTO diamondDto);
        Task UpdateDiamondAsync(DiamondDTO diamondDto);
        Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId, string color, string clarity, string cut, double? minPrice, double? maxPrice, double? minDiameter, double? maxDiameter, double? minWeight, double? maxWeight);
        Task<List<DiamondType>> GetAllDiamondTypes();
        Task<List<DiamondClarity>> GetAllDiamondClarities();
        Task<List<DiamondColor>> GetAllDiamondColors(); 
        Task<List<DiamondCut>> GetAllDiamondCuts();
        Task<Diamond> GetById(int id, string includeProperties = "");
        Task<List<Diamond>> GetRelatedDiamonds(int? diamondTypeId, int excludeDiamondId);

    }
}
