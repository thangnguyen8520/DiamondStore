using AutoMapper;
using DiamondBusinessObject.DTO;
using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using Microsoft.Extensions.Configuration;
using Net.payOS.Errors;
using Net.payOS.Types;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly PayOSPaymentService _payOSPaymentService;
        private readonly IUnitOfWork _unitOfWork;


        public PaymentService(IUnitOfWork unitOfWork, IPaymentRepository paymentRepository, ICartRepository cartRepository, IMapper mapper, IConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _payOSPaymentService = new PayOSPaymentService(
                configuration["PayOS:ClientId"],
                configuration["PayOS:ApiKey"],
                configuration["PayOS:ChecksumKey"]
            );
        }

        private float CalculateTotalPrice(Jewelry jewelry)
        {
            float mainDiamondPrice = jewelry.Diamond?.DiamondPrice ?? 0;
            float secondaryDiamondPrice = jewelry.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
            return 1.3f * (mainDiamondPrice + secondaryDiamondPrice + jewelry.JewelryPrice + jewelry.LaborCost);
        }

        public async Task<(string PaymentLink, int PaymentId)> ProcessPayment(string userId, AddPaymentDTO paymentDetails)
        {
            var user = await _paymentRepository.GetUserWithCarts(userId);
            if (user == null)
            {
                return (null, 0);
            }

            var activeCart = await _cartRepository.GetActiveCartByUserIdAsync(userId);
            if (activeCart == null)
            {
                activeCart = new Cart
                {
                    UserId = userId,
                    Status = true
                };
                _cartRepository.Add(activeCart); 
                await _cartRepository.SaveChangesAsync();
            }

            float totalAmount = activeCart.CartDiamonds.Sum(cd => cd.Diamond.DiamondPrice * cd.Quantity) +
                                activeCart.CartJewelries.Sum(cj => CalculateTotalPrice(cj.Jewelry) * cj.Quantity);
            float promotionDiscount = 0;

            foreach (var cartPromotion in activeCart.CartPromotions)
            {
                var promotion = cartPromotion.UserPromotion?.Promotion;
                if (promotion != null)
                {
                    promotionDiscount += (float)(activeCart.TotalPrice * promotion.DiscountRate / 100.0);
                }
            }

            totalAmount -= promotionDiscount;

            if (totalAmount <= 0)
            {
                return (null, 0);
            }

            var payment = new Payment
            {
                UserId = userId,
                FullName = $"{paymentDetails.FirstName} {paymentDetails.LastName}",
                PhoneNumber = paymentDetails.Phone,
                Email = paymentDetails.Email,
                Address = paymentDetails.Address,
                CreateDate = DateTime.Now,
                ProductName = "Cart Items",
                TotalAmount = totalAmount,
                Status = "Pending",
                PaymentMethodId = paymentDetails.PaymentMethodId
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            var cartItems = activeCart.CartDiamonds.Select(cd => new ItemData(
                name: cd.Diamond.DiamondName,
                quantity: cd.Quantity,
                price: (int)cd.Diamond.DiamondPrice
            )).ToList();

            cartItems.AddRange(activeCart.CartJewelries.Select(cj => new ItemData(
                name: cj.Jewelry.JewelryName,
                quantity: cj.Quantity,
                price: (int)CalculateTotalPrice(cj.Jewelry)
            )));

            const int bankAccountMethodId = 12;
            if (paymentDetails.PaymentMethodId == bankAccountMethodId)
            {
                var paymentPayload = new PaymentData(
                    orderCode: long.Parse(GenerateRandomNumericId()),
                    amount: (int)payment.TotalAmount,
                    description: "Order Payment",
                    items: cartItems,
                    cancelUrl: $"https://localhost:7243/Checkout?handler=PaymentCallback&success=false&paymentId={payment.PaymentId}",
                    returnUrl: $"https://localhost:7243/Checkout?handler=PaymentCallback&success=true&paymentId={payment.PaymentId}"
                );

                var createPaymentResult = await _payOSPaymentService.CreatePaymentLink(paymentPayload);
                payment.PaymentLink = createPaymentResult.checkoutUrl;

                await _paymentRepository.SaveChangesAsync();

                return (payment.PaymentLink, payment.PaymentId);
            }

            return (null, 0);
        }

        public async Task UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            if (payment != null)
            {
                payment.Status = status;
                await _paymentRepository.SaveChangesAsync();

                if (status == "Success")
                {
                    var activeCart = await _cartRepository.GetActiveCartByUserIdAsync(payment.UserId);
                    if (activeCart != null)
                    {
                        activeCart.Status = false;
                        await _cartRepository.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task HandlePaymentSuccessAsync(int paymentId)
        {
            await UpdatePaymentStatusAsync(paymentId, "Success");
        }

        public async Task HandlePaymentCancellationAsync(int paymentId)
        {
            await UpdatePaymentStatusAsync(paymentId, "Cancelled");
        }

        private static Random random = new Random();
        public static string GenerateRandomNumericId(int length = 5)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
                Status = payment.Status
            });
        }
    }
}
