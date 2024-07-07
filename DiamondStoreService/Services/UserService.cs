using AutoMapper;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
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
        private readonly IMemoryCache _cache;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
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
    }
}
