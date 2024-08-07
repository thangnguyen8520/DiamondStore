﻿using DiamondBusinessObject.Models;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentAdminRepository _paymentAdminRepository;

        public UnitOfWork(DiamondStoreContext context, 
            IUserRepository userRepository, 
            IImageRepository imageRepository, 
            IPaymentRepository paymentRepository,
            IPaymentAdminRepository paymentAdminRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
            _paymentRepository = paymentRepository;
            _paymentAdminRepository = paymentAdminRepository;
        }

        public IUserRepository UserRepository => _userRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository;
        public IPaymentAdminRepository PaymentAdminRepository => _paymentAdminRepository;
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
