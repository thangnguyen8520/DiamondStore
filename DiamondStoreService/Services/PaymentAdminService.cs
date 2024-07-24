using AutoMapper;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class PaymentAdminService : IPaymentAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAsync(includeProperties: "PaymentMethod,User");
            return payments.Select(payment => new PaymentDTO
            {
                PaymentId = payment.PaymentId,
                FullName = payment.FullName,
                PhoneNumber = payment.PhoneNumber,
                Email = payment.Email,
                ProductName = payment.ProductName,
                PaymentMethodName = payment.PaymentMethod.PaymentMethodName,
                TotalAmount = payment.TotalAmount,
                Status = payment.Status,
                Address = payment.Address
            });
        }
    }
}
