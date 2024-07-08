﻿using AutoMapper;
using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserProfileViewDTO> GetUserByIdAsync(string userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, "Image");
            if (user == null) return null;

            return new UserProfileViewDTO
            {
                UserId = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.Image?.ImageUrl
            };
        }

        public async Task<ResponseModel> UpdateUserProfileAsync(string userId, UserProfileViewDTO updatedProfile)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ResponseModel { Success = false, ErrorMessage = "User not found." };
            }

            _mapper.Map(updatedProfile, user);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel { Success = true };
        }

        public async Task<ResponseModel> UpdateUserAvatarAsync(string userId, string imageUrl)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ResponseModel { Success = false, ErrorMessage = "User not found." };
            }

            if (user.ImageId.HasValue)
            {
                var image = await _unitOfWork.ImageRepository.GetByIdAsync(user.ImageId.Value);
                if (image != null)
                {
                    image.ImageUrl = imageUrl;
                    _unitOfWork.ImageRepository.Update(image);
                }
                else
                {
                    return new ResponseModel { Success = false, ErrorMessage = "Image not found." };
                }
            }
            else
            {
                var newImage = new Image { ImageUrl = imageUrl };
                await _unitOfWork.ImageRepository.AddAsync(newImage);
                await _unitOfWork.SaveChangeAsync();

                user.ImageId = newImage.Id;
            }

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync(); 

            return new ResponseModel { Success = true };
        }

        public async Task<ResponseModel> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseModel { Success = false, ErrorMessage = "User not found." };
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);
            if (!passwordCheck)
            {
                return new ResponseModel { Success = false, ErrorMessage = "Current password is incorrect." };
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return new ResponseModel { Success = false, ErrorMessage = $"Failed to change password: {errors}" };
            }

            return new ResponseModel { Success = true };
        }

        public async Task<List<OrderHistoryDTO>> GetOrderHistoryAsync(string userId)
        {
            // Lấy dữ liệu từ repository với các sản phẩm kim cương
            var payments = await _unitOfWork.PaymentRepository.GetAsync(
                filter: p => p.UserId == userId,
                orderBy: q => q.OrderByDescending(p => p.CreateDate),
                includeProperties: "PaymentDiamonds.Diamond"
            );

            // Chuyển đổi từ Payment sang OrderHistoryDTO
            var orderHistory = payments.Select(payment => new OrderHistoryDTO
            {
                PaymentId = payment.PaymentId,
                ProductName = payment.ProductName,
                TotalAmount = payment.TotalAmount,
                Status = payment.Status,
                CreateDate = payment.CreateDate,
                FuName = payment.FullName,
                Email = payment.Email,
                PhoneNumber = payment.PhoneNumber,
                Address = payment.Address,
                PaymentDiamonds = payment.PaymentDiamonds.Select(pd => new PaymentDiamondDTO
                {
                    PaymentDiamondId = pd.PaymentDiamondId,
                    DiamondName = pd.Diamond.DiamondName,
                    CaratWeight = pd.Diamond.CaratWeight,
                    Color = pd.Diamond.Color,
                    Clarity = pd.Diamond.Clarity,
                }).ToList()
            }).ToList();

            return orderHistory;
        }

        public async Task<OrderHistoryDTO> GetOrderDetailsAsync(int orderId)
        {
            var payment = await _unitOfWork.PaymentRepository
                                            .GetByIdAsync(orderId, "PaymentDiamonds.Diamond");

            if (payment == null)
            {
                return null;
            }

            var orderHistoryDTO = new OrderHistoryDTO
            {
                PaymentId = payment.PaymentId,
                ProductName = payment.ProductName,
                TotalAmount = payment.TotalAmount,
                Status = payment.Status,
                CreateDate = payment.CreateDate,
                FuName = payment.FullName,
                Email = payment.Email,
                PhoneNumber = payment.PhoneNumber,
                Address = payment.Address,
                PaymentDiamonds = payment.PaymentDiamonds.Select(d => new PaymentDiamondDTO
                {
                    PaymentDiamondId = d.PaymentDiamondId,
                    DiamondName = d.Diamond.DiamondName,
                    CaratWeight = d.Diamond.CaratWeight,
                    Color = d.Diamond.Color,
                    Clarity = d.Diamond.Clarity
                }).ToList()
            };

            return orderHistoryDTO;
        }


    }
}
