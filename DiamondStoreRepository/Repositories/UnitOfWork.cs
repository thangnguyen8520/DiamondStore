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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DiamondStoreContext _context;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(DiamondStoreContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
