using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await _paymentMethodRepository.GetAllPaymentMethodsAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int id)
        {
            return await _paymentMethodRepository.GetPaymentMethodByIdAsync(id);
        }

        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _paymentMethodRepository.AddPaymentMethodAsync(paymentMethod);
        }

        public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _paymentMethodRepository.UpdatePaymentMethodAsync(paymentMethod);
        }

        public async Task DeletePaymentMethodAsync(int id)
        {
            await _paymentMethodRepository.DeletePaymentMethodAsync(id);
        }
    }
}
