﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IImageRepository ImageRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IPaymentAdminRepository PaymentAdminRepository { get; }
        Task<int> SaveChangeAsync();
    }
}
