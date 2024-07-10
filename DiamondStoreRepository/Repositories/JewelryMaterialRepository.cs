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
    public class JewelryMaterialRepository : GenericRepository<JewelryMaterial>, IJewelryMaterialRepository
    {
        private readonly DiamondStoreContext _context;

        public JewelryMaterialRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<JewelryMaterial>> GetAllJewelryMaterials()
        {
            return await _context.JewelryMaterials.ToListAsync();
        }
    }
}
