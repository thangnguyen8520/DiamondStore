using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileViewDTO> GetUserByIdAsync(string userId);
        Task<ResponseModel> UpdateUserProfileAsync(string userId, UserProfileViewDTO updatedProfile);
        Task<ResponseModel> UpdateUserAvatarAsync(string userId, string imageUrl);
        Task<ResponseModel> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto);
        //Task<List<OrderHistoryDTO>> GetOrderHistoryAsync(string userId);
        //Task<OrderHistoryDTO> GetOrderDetailsAsync(int orderId);

        Task<IEnumerable<UserDTO>> GetAllActiveUsersAsync();
        Task<UserDTO> GetUserInAdminByIdAsync(string userId);
        Task<bool> UpdateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(string userId);
        Task<bool> UpdateUserStatusAsync(string userId, bool status);
        Task<UserDTO> GetUserDetailAsync(string userId);
        Task<List<UserDTO>> GetUserAsync(string userId);

    }
}
