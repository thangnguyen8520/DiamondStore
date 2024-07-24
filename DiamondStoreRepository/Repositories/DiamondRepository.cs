using DiamondBusinessObject.Models;
using DiamondStoreRepository.Common;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class DiamondRepository : GenericRepository<Diamond>, IDiamondRepository
    {
        private readonly DiamondStoreContext _context;

        public DiamondRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Diamond>> GetAllAsync()
        {
            return await _context.Diamonds
                                 .Include(d => d.DiamondColor)
                                 .Include(d => d.DiamondClarity)
                                 .ToListAsync();
        }

        public async Task<Diamond> GetDiamondByIdAsync(int id)
        {
            return await _context.Diamonds
                                 .Include(d => d.DiamondColor)
                                 .Include(d => d.DiamondClarity)
                                 .Include(d => d.DiamondCut)
                                 .Include(d => d.DiamondType)
                                 .FirstOrDefaultAsync(d => d.DiamondId == id);
        }

        public async Task AddDiamondAsync(Diamond diamond)
        {
            await _context.Diamonds.AddAsync(diamond);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDiamondAsync(Diamond diamond)
        {
            _context.Diamonds.Update(diamond);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDiamondAsync(int id)
        {
            var diamond = await _context.Diamonds.FindAsync(id);
            if (diamond != null)
            {
                _context.Diamonds.Remove(diamond);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Pagination<Diamond>> GetDiamonds(int pageIndex, int pageSize, string sortOption, int? categoryId, string color, string clarity, string cut, double? minPrice, double? maxPrice, double? minDiameter, double? maxDiameter, double? minWeight, double? maxWeight)
        {
            IQueryable<Diamond> query = _context.Diamonds
                                                .Include(d => d.DiamondColor)
                                                .Include(d => d.DiamondClarity)
                                                .Include(d => d.DiamondCut)
                                                .Include(d => d.DiamondType);

            if (categoryId.HasValue)
                query = query.Where(d => d.DiamondTypeId == categoryId.Value);

            if (!string.IsNullOrEmpty(color))
                query = query.Where(d => d.DiamondColor.DiamondColorName == color);

            if (!string.IsNullOrEmpty(clarity))
                query = query.Where(d => d.DiamondClarity.DiamondClarityName == clarity);

            if (!string.IsNullOrEmpty(cut))
                query = query.Where(d => d.DiamondCut.DiamondCutName == cut);

            if (minPrice.HasValue)
                query = query.Where(d => d.DiamondPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(d => d.DiamondPrice <= maxPrice.Value);

            if (minDiameter.HasValue)
                query = query.Where(d => d.DiamondDiameter >= minDiameter.Value);

            if (maxDiameter.HasValue)
                query = query.Where(d => d.DiamondDiameter <= maxDiameter.Value);

            if (minWeight.HasValue)
                query = query.Where(d => d.DiamondWeight >= minWeight.Value);

            if (maxWeight.HasValue)
                query = query.Where(d => d.DiamondWeight <= maxWeight.Value);

            switch (sortOption)
            {
                case "PriceLowToHigh":
                    query = query.OrderBy(d => d.DiamondPrice);
                    break;
                case "PriceHighToLow":
                    query = query.OrderByDescending(d => d.DiamondPrice);
                    break;
                case "DateNewToOld":
                    query = query.OrderByDescending(d => d.CreateDate);
                    break;
                case "DateOldToNew":
                    query = query.OrderBy(d => d.CreateDate);
                    break;
                case "AlphabeticalAZ":
                    query = query.OrderBy(d => d.DiamondName);
                    break;
                case "AlphabeticalZA":
                    query = query.OrderByDescending(d => d.DiamondName);
                    break;
                default:
                    query = query.OrderByDescending(d => d.CreateDate);
                    break;
            }

            return await ToPaginationAsync(query, pageIndex, pageSize);
        }


        public async Task<List<DiamondType>> GetAllDiamondTypes()
        {
            return await _context.DiamondTypes.ToListAsync();
        }

        public async Task<List<DiamondClarity>> GetAllDiamondClarities()
        {
            return await _context.DiamondClarities.ToListAsync();
        }

        public async Task<List<DiamondColor>> GetAllDiamondColors()
        {
            return await _context.DiamondColors.ToListAsync();
        }

        public async Task<List<DiamondCut>> GetAllDiamondCuts()
        {
            return await _context.DiamondCuts.ToListAsync();
        }

        public async Task<Diamond> GetById(int id, string includeProperties = "")
        {
            IQueryable<Diamond> query = _context.Diamonds;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(d => d.DiamondId == id);
        }

        public async Task<List<Diamond>> GetRelatedDiamonds(int? diamondTypeId, int excludeDiamondId)
        {
            return await _dbSet
                .Where(d => d.DiamondTypeId == diamondTypeId && d.DiamondId != excludeDiamondId)
                .Take(4)
                .Include(d => d.Image)
                .ToListAsync();
        }
    }
}
