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

        public async Task<IEnumerable<Jewelry>> GetAllAsync()
        {
            return await _context.Jewelries
                                 .Include(j => j.JewelryMaterial)
                                 .Include(j => j.JewelryType)
                                 .Include(j => j.Diamond)
                                 .Include(j => j.SecondaryDiamonds)
                                 .ThenInclude(d => d.Diamond)
                                 .ToListAsync();
        }

        public float CalculateTotalPrice(Jewelry jewelry)
        {
            float mainDiamondPrice = jewelry.Diamond?.DiamondPrice ?? 0;
            float secondaryDiamondPrice = jewelry.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
            return 1.3f * (mainDiamondPrice + secondaryDiamondPrice + jewelry.JewelryPrice + jewelry.LaborCost);
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
                item.TotalPrice = CalculateTotalPrice(item);
            }

            if (!string.IsNullOrEmpty(priceRange))
            {
                var ranges = priceRange.Split('-');
                double minPrice = double.Parse(ranges[0]);
                double maxPrice = ranges[1] == "9999999999" ? double.MaxValue : double.Parse(ranges[1]);
                items = items.Where(j => j.TotalPrice >= minPrice && j.TotalPrice <= maxPrice).ToList();
            }

            items = sortOption switch
            {
                "PriceLowToHigh" => items.OrderBy(j => j.TotalPrice).ToList(),
                "PriceHighToLow" => items.OrderByDescending(j => j.TotalPrice).ToList(),
                "AlphabeticalAZ" => items.OrderBy(j => j.JewelryName).ToList(),
                "AlphabeticalZA" => items.OrderByDescending(j => j.JewelryName).ToList(),
                "DateNewToOld" => items.OrderByDescending(j => j.CreateDate).ToList(),
                "DateOldToNew" => items.OrderBy(j => j.CreateDate).ToList(),
                _ => items.OrderByDescending(j => j.CreateDate).ToList(),
            };

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
                .Include(j => j.SecondaryDiamonds).ThenInclude(sd => sd.Diamond)
                .Include(j => j.JewelryMaterial)
                .Include(j => j.Image)
                .FirstOrDefaultAsync(j => j.JewelryId == jewelryId);

            if (jewelry != null)
            {
                jewelry.TotalPrice = CalculateTotalPrice(jewelry);
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
                item.TotalPrice = CalculateTotalPrice(item);
            }

            return items;
        }

        public async Task<Jewelry> GetJewelryByIdAsync(int id)
        {
            return await _context.Jewelries
                                 .Include(j => j.JewelryMaterial)
                                 .Include(j => j.JewelryType)
                                 .Include(j => j.Diamond)
                                 .Include(j => j.SecondaryDiamonds)
                                 .ThenInclude(d => d.Diamond)
                                 .FirstOrDefaultAsync(j => j.JewelryId == id);
        }

        public async Task AddJewelryAsync(Jewelry jewelry)
        {
            await _context.Jewelries.AddAsync(jewelry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJewelryAsync(Jewelry jewelry)
        {
            _context.Jewelries.Update(jewelry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJewelryAsync(int id)
        {
            var jewelry = await _context.Jewelries.FindAsync(id);
            if (jewelry != null)
            {
                _context.Jewelries.Remove(jewelry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
