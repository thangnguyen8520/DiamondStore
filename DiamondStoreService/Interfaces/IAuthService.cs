using DiamondStoreService.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(LoginRequest request, HttpContext httpContext);
        //Task<string> Register(string username, string password);
    }
}
