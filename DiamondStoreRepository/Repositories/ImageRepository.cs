using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly DiamondStoreContext _context;

        public ImageRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
