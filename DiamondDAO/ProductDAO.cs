using DiamondBusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondDAO
{
    public class ProductDAO
    {
        private readonly DiamondStoreContext _context;

        public ProductDAO(DiamondStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Diamond>> GetAllDiamonds()
        {
            return await _context.Diamonds
                                 .Include(d => d.DiamondType)
                                 .ToListAsync();
        }

        public async Task<List<DiamondType>> GetAllDiamondTypes()
        {
            return await _context.DiamondTypes.ToListAsync();
        }
    }
}
