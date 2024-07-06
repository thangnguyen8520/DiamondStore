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
        private readonly IImageRepository _imageRepository;

        public UnitOfWork(DiamondStoreContext context, IUserRepository userRepository, IImageRepository imageRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }

        public IUserRepository UserRepository => _userRepository;
        public IImageRepository ImageRepository => _imageRepository;

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
