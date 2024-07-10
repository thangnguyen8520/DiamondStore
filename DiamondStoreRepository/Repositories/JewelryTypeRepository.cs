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
    public class JewelryTypeRepository : GenericRepository<JewelryType>, IJewelryTypeRepository
    {
        private readonly DiamondStoreContext _context;

        public JewelryTypeRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<JewelryType>> GetAllJewelryTypes()
        {
            return await _context.JewelryTypes.ToListAsync();
        }
    }
}
