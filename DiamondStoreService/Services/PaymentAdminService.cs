using AutoMapper;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using System.Collections.Generic;
using System.Linq;
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
            var payments = await _unitOfWork.PaymentRepository.GetAsync(includeProperties: "PaymentMethod,User,Cart.CartDiamonds.Diamond,Cart.CartJewelries.Jewelry");
            return payments.Select(payment => new PaymentDTO
            {
                PaymentId = payment.PaymentId,
                OrderDate = payment.CreateDate ?? DateTime.MinValue,
                FullName = payment.FullName ?? "Unknown",
                PhoneNumber = payment.PhoneNumber ?? "Unknown",
                Email = payment.Email ?? "Unknown",
                ProductName = payment.ProductName ?? "Unknown",
                PaymentMethodName = payment.PaymentMethod?.PaymentMethodName ?? "Unknown",
                TotalAmount = payment.TotalAmount,
                Status = payment.Status ?? "Unknown",
                Address = payment.Address ?? "Unknown",
                CartDiamonds = payment.Cart.CartDiamonds.Select(d => new PaymentDiamondDTO
                {
                    Type = "Diamond",
                    ProductName = d.Diamond?.DiamondName ?? "Unknown",
                    Quantity = d.Quantity,
                    Price = d.Diamond?.DiamondPrice ?? 0,
                    Total = d.Diamond?.DiamondPrice ?? 0 * d.Quantity
                }).ToList(),
                CartJewelries = payment.Cart.CartJewelries.Select(j => new PaymentJewelryDTO
                {
                    Type = "Jewelry",
                    ProductName = j.Jewelry?.JewelryName ?? "Unknown",
                    Quantity = j.Quantity,
                    Price = j.Jewelry?.TotalPrice ?? 0,
                    Total = j.Jewelry?.TotalPrice ?? 0 * j.Quantity
                }).ToList()
            });
        }

        public async Task<PaymentDTO> GetPaymentDetailsAsync(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id, includeProperties: "Cart.CartDiamonds.Diamond,Cart.CartJewelries.Jewelry");

            if (payment == null)
            {
                return null;
            }

            var paymentDTO = new PaymentDTO
            {
                PaymentId = payment.PaymentId,
                OrderDate = payment.CreateDate ?? DateTime.MinValue,
                FullName = payment.FullName ?? "Unknown",
                PhoneNumber = payment.PhoneNumber ?? "Unknown",
                Email = payment.Email ?? "Unknown",
                ProductName = payment.ProductName ?? "Unknown",
                PaymentMethodName = payment.PaymentMethod?.PaymentMethodName ?? "Unknown",
                TotalAmount = payment.TotalAmount,
                Status = payment.Status ?? "Unknown",
                Address = payment.Address ?? "Unknown",
                CartDiamonds = payment.Cart.CartDiamonds?.Select(d => new PaymentDiamondDTO
                {
                    Type = "Diamond",
                    ProductName = d.Diamond?.DiamondName ?? "Unknown",
                    Quantity = d.Quantity,
                    Price = d.Diamond?.DiamondPrice ?? 0,
                    Total = (d.Diamond?.DiamondPrice ?? 0) * d.Quantity
                }).ToList(),
                CartJewelries = payment.Cart.CartJewelries?.Select(j => new PaymentJewelryDTO
                {
                    Type = "Jewelry",
                    ProductName = j.Jewelry?.JewelryName ?? "Unknown",
                    Quantity = j.Quantity,
                    Price = j.Jewelry?.TotalPrice ?? 0,
                    Total = (j.Jewelry?.TotalPrice ?? 0) * j.Quantity
                }).ToList()
            };

            return paymentDTO;
        }


    }
}
