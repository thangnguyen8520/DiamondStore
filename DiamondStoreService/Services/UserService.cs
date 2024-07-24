using AutoMapper;
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
            // Fetch the payments with necessary related data
            var payments = await _unitOfWork.PaymentAdminRepository.GetAsync(
                filter: p => p.UserId == userId,
                orderBy: q => q.OrderByDescending(p => p.CreateDate),
                includeProperties: "Cart.CartDiamonds.Diamond,Cart.CartJewelries.Jewelry"
            );

            // Map payments to OrderHistoryDTO
            var orderHistory = payments.Select(payment => new OrderHistoryDTO
            {
                PaymentId = payment.PaymentId,
                ProductName = payment.ProductName ?? "Unknown",
                TotalAmount = (decimal)(payment.TotalAmount ?? 0),
                Status = payment.Status ?? "Unknown",
                CreateDate = payment.CreateDate,
                FuName = payment.FullName ?? "N/A",
                Email = payment.Email ?? "N/A",
                PhoneNumber = payment.PhoneNumber ?? "N/A",
                Address = payment.Address ?? "N/A",
                Cash = 0,
                BankTransfer = (decimal)(payment.TotalAmount ?? 0),
                Subtotal = (decimal)(payment.TotalAmount ?? 0),
                Discount = "0",
                ShippingFee = "Free",
                PaymentDiamonds = payment.Cart?.CartDiamonds.Select(d => new PaymentDiamondDTO
                {
                    Type = "Diamond",
                    ProductName = d.Diamond?.DiamondName ?? "Unknown",
                    Quantity = d.Quantity,
                    Price = d.Diamond?.DiamondPrice ?? 0,
                    Total = (d.Diamond?.DiamondPrice ?? 0) * d.Quantity
                }).ToList() ?? new List<PaymentDiamondDTO>(),
                PaymentJewelries = payment.Cart?.CartJewelries.Select(j => new PaymentJewelryDTO
                {
                    Type = "Jewelry",
                    ProductName = j.Jewelry?.JewelryName ?? "Unknown",
                    Quantity = j.Quantity,
                    Price = j.Jewelry?.TotalPrice ?? 0,
                    Total = (j.Jewelry?.TotalPrice ?? 0) * j.Quantity
                }).ToList() ?? new List<PaymentJewelryDTO>()
            }).ToList();

            return orderHistory;
        }


        public async Task<OrderHistoryDTO> GetOrderDetailsAsync(int orderId)
        {
            var payment = await _unitOfWork.PaymentAdminRepository
                                            .GetByIdAsync(orderId, "Cart.CartDiamonds.Diamond,Cart.CartJewelries.Jewelry");

            if (payment == null)
            {
                return null;
            }

            var orderHistoryDTO = new OrderHistoryDTO
            {
                PaymentId = payment.PaymentId,
                ProductName = payment.ProductName ?? "Unknown",
                TotalAmount = (decimal)(payment.TotalAmount ?? 0),
                Status = payment.Status ?? "Unknown",
                CreateDate = payment.CreateDate,
                FuName = payment.FullName ?? "N/A",
                Email = payment.Email ?? "N/A",
                PhoneNumber = payment.PhoneNumber ?? "N/A",
                Address = payment.Address ?? "N/A",
                Cash = 0,
                BankTransfer = (decimal)(payment.TotalAmount ?? 0),
                Subtotal = (decimal)(payment.TotalAmount ?? 0),
                Discount = "0",
                ShippingFee = "Free",
                PaymentDiamonds = payment.Cart?.CartDiamonds.Select(d => new PaymentDiamondDTO
                {
                    Type = "Diamond",
                    ProductName = d.Diamond?.DiamondName ?? "Unknown",
                    Quantity = d.Quantity,
                    Price = d.Diamond?.DiamondPrice ?? 0,
                    Total = (d.Diamond?.DiamondPrice ?? 0) * d.Quantity
                }).ToList() ?? new List<PaymentDiamondDTO>(),
                PaymentJewelries = payment.Cart?.CartJewelries.Select(j => new PaymentJewelryDTO
                {
                    Type = "Jewelry",
                    ProductName = j.Jewelry?.JewelryName ?? "Unknown",
                    Quantity = j.Quantity,
                    Price = j.Jewelry?.TotalPrice ?? 0,
                    Total = (j.Jewelry?.TotalPrice ?? 0) * j.Quantity
                }).ToList() ?? new List<PaymentJewelryDTO>()
            };

            return orderHistoryDTO;
        }




        public async Task<IEnumerable<UserDTO>> GetAllActiveUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAsync(u => u.Status == true, includeProperties: "Image");
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                if (role != "Admin")
                {
                    var userDTO = new UserDTO
                    {
                        ImageUrl = user.Image?.ImageUrl,
                        FullName = $"{user.FirstName} {user.LastName}",
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role,
                        LastLogin = user.LastLogin,
                        Gender = user.Gender.HasValue ? (user.Gender.Value ? "Female" : "Male") : "Unknown",
                        Status = user.Status.HasValue ? (user.Status.Value ? "Active" : "Blocked") : "Unknown",
                    };

                    userDTOs.Add(userDTO);
                }
            }

            return userDTOs;
        }


        //public async Task<UserDTO> GetUserInAdminByIdAsync(string userId)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByIdNoIncludeAsync(userId);
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var userDto = new UserDTO
        //    {
        //        Id = user.Id,
        //        FullName = $"{user.FirstName} {user.LastName}",
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        Gender = user.Gender == true ? "Female" : "Male",
        //        Status = user.Status == true ? "Active" : "Blocked",
        //        Address = user.Address,
        //        LastLogin = user.LastLogin,
        //        ImageUrl = user.Image?.ImageUrl
        //    };
        //    return userDto;
        //}
        //public async Task<List<UserDTO>> GetUserAsync(string userId)
        //{
        //    var user = await _unitOfWork.UserRepository.GetAsync(
        //        filter: p => p.Id == userId,
        //        orderBy: q => q.OrderByDescending(p => p.LastLogin));
        //    var userDto = user.Select(user => new UserDTO
        //    {
        //        Id = user.Id,
        //        FullName = $"{user.FirstName} {user.LastName}",
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        Gender = user.Gender == true ? "Female" : "Male",
        //        Status = user.Status == true ? "Active" : "Blocked",
        //        Address = user.Address,
        //        LastLogin = user.LastLogin,
        //        ImageUrl = user.Image?.ImageUrl
        //    }).ToList();
        //    return userDto;
        //}
        //public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByIdAsync(userDTO.Id);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    // Map the changes from userDTO to the user entity
        //    _mapper.Map(userDTO, user);

        //    _unitOfWork.UserRepository.Update(user);
        //    await _unitOfWork.SaveChangeAsync();

        //    return true;
        //}

        //public async Task DeleteUserAsync(string userId)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        //    if (user != null)
        //    {
        //        user.Status = false;
        //        _unitOfWork.UserRepository.Update(user);
        //        await _unitOfWork.UserRepository.SaveAsync(user);
        //    }
        //}
        //public async Task<bool> UpdateUserStatusAsync(string userId, bool status)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    user.Status = status;
        //    await _unitOfWork.UserRepository.UpdateTaskAsync(user);
        //    await _unitOfWork.SaveChangeAsync();

        //    return true;
        //}
        //public async Task<UserDTO> GetUserDetailAsync(string userId)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, includeProperties: "Image");
        //    if (user == null) return null;

        //    return new UserDTO
        //    {
        //        Id = user.Id,
        //        FullName = $"{user.FirstName} {user.LastName}",
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        Gender = user.Gender == true ? "Female" : "Male",
        //        Status = user.Status == true ? "Active" : "Blocked",
        //        Address = user.Address,
        //        LastLogin = user.LastLogin,
        //        ImageUrl = user.Image?.ImageUrl
        //    };
        //}


    }
}