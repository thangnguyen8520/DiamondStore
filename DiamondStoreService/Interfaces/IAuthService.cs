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
        Task<LoginResult> Login(LoginRequest request, HttpContext httpContext);
        Task<IdentityResult> Register(RegisterRequest request, HttpContext httpContext);
        Task<bool> ConfirmEmailAsync(string token, string email);
        Task<bool> ResendConfirmationEmailAsync(string email, HttpContext httpContext);
    }
}
