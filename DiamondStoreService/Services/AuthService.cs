using AutoMapper;
using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Login(LoginRequest request, HttpContext httpContext)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return false;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(user);

            httpContext.Session.SetString("UserId", user.Id);
            httpContext.Session.SetString("Email", user.Email);
            httpContext.Session.SetString("Roles", string.Join(",", roles));

            return true;
        }
    }
}
