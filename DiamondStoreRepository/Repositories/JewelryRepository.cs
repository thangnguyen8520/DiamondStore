using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DiamondStoreRepository.Repositories
{
    public class JewelryRepository : GenericRepository<Jewelry>, IJewelryRepository
    {
        private readonly DiamondStoreContext _context;

        public JewelryRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pagination<Jewelry>> GetPaginated(int pageIndex, int pageSize, string sortOption, int? typeId, string material, int? sizeId, string priceRange)
        {
            var query = _context.Jewelries
                                .Include(j => j.Diamond)
                                .Include(j => j.SecondaryDiamonds).ThenInclude(sd => sd.Diamond)
                                .AsQueryable();

            if (typeId.HasValue)
            {
                query = query.Where(j => j.JewelryTypeId == typeId);
            }

            if (!string.IsNullOrEmpty(material))
            {
                query = query.Where(j => j.JewelryMaterial.JewelryMaterialName == material);
            }

            var items = await query.ToListAsync();

            // Calculate TotalPrice for each item
            foreach (var item in items)
            {
                float mainDiamondPrice = item.Diamond?.DiamondPrice ?? 0;
                float secondaryDiamondPrice = item.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
                item.TotalPrice = 1.3f * (mainDiamondPrice + secondaryDiamondPrice + item.JewelryPrice + item.LaborCost);
            }

            if (!string.IsNullOrEmpty(priceRange))
            {
                var ranges = priceRange.Split('-');
                double minPrice = double.Parse(ranges[0]);
                double maxPrice = ranges[1] == "9999999999" ? double.MaxValue : double.Parse(ranges[1]);
                items = items.Where(j => j.TotalPrice >= minPrice && j.TotalPrice <= maxPrice).ToList();
            }

            switch (sortOption)
            {
                case "PriceLowToHigh":
                    items = items.OrderBy(j => j.TotalPrice).ToList();
                    break;
                case "PriceHighToLow":
                    items = items.OrderByDescending(j => j.TotalPrice).ToList();
                    break;
                case "AlphabeticalAZ":
                    items = items.OrderBy(j => j.JewelryName).ToList();
                    break;
                case "AlphabeticalZA":
                    items = items.OrderByDescending(j => j.JewelryName).ToList();
                    break;
                case "DateNewToOld":
                    items = items.OrderByDescending(j => j.CreateDate).ToList();
                    break;
                case "DateOldToNew":
                    items = items.OrderBy(j => j.CreateDate).ToList();
                    break;
                default:
                    items = items.OrderByDescending(j => j.CreateDate).ToList();
                    break;
            }

            var totalItems = items.Count;
            items = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new Pagination<Jewelry>
            {
                Items = items,
                TotalItemsCount = totalItems,
                PageIndex = pageIndex > 0 ? pageIndex : 1,
                PageSize = pageSize
            };
        }

        public async Task<Jewelry> GetJewelryWithDetails(int jewelryId)
        {
            var jewelry = await _context.Jewelries
                .Include(j => j.Diamond)
                .Include(j => j.SecondaryDiamonds)
                    .ThenInclude(sd => sd.Diamond)
                .Include(j => j.JewelryMaterial)
                .Include(j => j.Image)
                .FirstOrDefaultAsync(j => j.JewelryId == jewelryId);

            if (jewelry != null)
            {
                float mainDiamondPrice = jewelry.Diamond?.DiamondPrice ?? 0;
                float secondaryDiamondPrice = jewelry.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
                jewelry.TotalPrice = 1.3f * (mainDiamondPrice + secondaryDiamondPrice + jewelry.JewelryPrice + jewelry.LaborCost);
            }

            return jewelry;
        }

        public async Task<List<Jewelry>> GetRelatedJewelries(int typeId, int excludeId)
        {
            var query = _context.Jewelries
                .Include(j => j.Diamond)
                 .Include(j => j.SecondaryDiamonds).ThenInclude(sd => sd.Diamond)
                .Where(j => j.JewelryTypeId == typeId && j.JewelryId != excludeId)
                .Take(4)
                .Include(d => d.Image)
                .AsQueryable();

            var items = await query.ToListAsync();
            foreach (var item in items)
            {
                float mainDiamondPrice = item.Diamond?.DiamondPrice ?? 0;
                float secondaryDiamondPrice = item.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
                item.TotalPrice = 1.3f * (mainDiamondPrice + secondaryDiamondPrice + item.JewelryPrice + item.LaborCost);
            }

            return items;
        }
    }
}
