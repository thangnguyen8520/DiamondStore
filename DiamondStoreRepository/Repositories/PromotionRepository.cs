using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly DiamondStoreContext _context;

        public PromotionRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserPromotion>> GetUserPromotions(string userId)
        {
            return await _context.UserPromotions
                .Where(up => up.UserId == userId)
                .Include(up => up.Promotion)
                .ToListAsync();
        }
    }
}
