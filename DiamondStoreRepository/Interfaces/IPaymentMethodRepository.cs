using DiamondBusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int id);
        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task DeletePaymentMethodAsync(int id);
    }
}
