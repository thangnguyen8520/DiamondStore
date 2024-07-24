using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(DiamondStoreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _dbSet.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            _dbSet.Update(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentMethodAsync(int id)
        {
            var paymentMethod = await _dbSet.FindAsync(id);
            if (paymentMethod != null)
            {
                _dbSet.Remove(paymentMethod);
                await _context.SaveChangesAsync();
            }
        }
    }
}
