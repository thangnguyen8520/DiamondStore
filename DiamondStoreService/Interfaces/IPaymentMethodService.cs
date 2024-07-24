using DiamondBusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int id);
        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task DeletePaymentMethodAsync(int id);
    }
}
