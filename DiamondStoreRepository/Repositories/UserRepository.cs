using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DiamondStoreContext _context;

        public UserRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
