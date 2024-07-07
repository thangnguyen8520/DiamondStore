using DiamondStoreService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginRequest request);
        Task<IdentityResult> Register(RegisterRequest request);
        Task<bool> ConfirmEmailAsync(string token, string email);
        Task<bool> ResendConfirmationEmailAsync(string email);
        Task<LoginResult> HandleGoogleCallbackAndSaveAsync();
        Task<ResponseModel> SendForgotPasswordEmailAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
