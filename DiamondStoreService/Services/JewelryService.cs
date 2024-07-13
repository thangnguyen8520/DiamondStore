﻿using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using DiamondStoreRepository.Repositories;
using DiamondStoreService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class JewelryService : IJewelryService
    {
        private readonly IJewelryRepository _jewelryRepository;
        private readonly IJewelryTypeRepository _jewelryTypeRepository;
        private readonly IJewelryMaterialRepository _jewelryMaterialRepository;
        private readonly IJewelrySizeRepository _jewelrySizeRepository;

        public JewelryService(
            IJewelryRepository jewelryRepository,
            IJewelryTypeRepository jewelryTypeRepository,
            IJewelryMaterialRepository jewelryMaterialRepository,
            IJewelrySizeRepository jewelrySizeRepository)
        {
            _jewelryRepository = jewelryRepository;
            _jewelryTypeRepository = jewelryTypeRepository;
            _jewelryMaterialRepository = jewelryMaterialRepository;
            _jewelrySizeRepository = jewelrySizeRepository;
        }

        public async Task<Pagination<Jewelry>> GetJewelries(int pageIndex, int pageSize, string sortOption, int? typeId, string material, int? sizeId, string priceRange)
        {
            return await _jewelryRepository.GetPaginated(pageIndex, pageSize, sortOption, typeId, material, sizeId, priceRange);
        }

        public async Task<Jewelry> GetJewelryWithDetails(int jewelryId)
        {
            return await _jewelryRepository.GetJewelryWithDetails(jewelryId);
        }

        public async Task<IList<JewelryType>> GetAllJewelryTypes()
        {
            return await _jewelryTypeRepository.GetAllJewelryTypes();
        }

        public async Task<IList<JewelryMaterial>> GetAllJewelryMaterials()
        {
            return await _jewelryMaterialRepository.GetAllJewelryMaterials();
        }

        public async Task<IList<JewelrySize>> GetAllJewelrySizes()
        {
            return await _jewelrySizeRepository.GetAllJewelrySizes();
        }

        public async Task<List<Jewelry>> GetRelatedJewelries(int typeId, int excludeId)
        {
            return await _jewelryRepository.GetRelatedJewelries(typeId, excludeId);
        }
    }

}
