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
    public class JewelrySizeRepository : GenericRepository<JewelrySize>, IJewelrySizeRepository
    {
        private readonly DiamondStoreContext _context;

        public JewelrySizeRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<JewelrySize>> GetAllJewelrySizes()
        {
            return await _context.JewelrySizes.ToListAsync();
        }
    }
}
