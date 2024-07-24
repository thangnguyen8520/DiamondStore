using DiamondBusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<User> GetUserWithCarts(string userId);
        Task UpdatePayment(Payment payment);
        Task SaveChangesAsync();
    }
}
