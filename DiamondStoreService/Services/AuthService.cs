using AutoMapper;
using Azure.Core;
using DiamondBusinessObject.Enums;
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
using System.Net.Http;
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
        private readonly IEmailSender _emailSender;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache, UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        #region Login
        public async Task<LoginResult> Login(LoginRequest request, HttpContext httpContext)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResult
                {
                    Succeeded = false,
                    ErrorMessage = "User not found."
                };
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResult
                {
                    Succeeded = false,
                    ErrorMessage = "Email not confirmed."
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!signInResult.Succeeded)
            {
                string errorMessage = signInResult.IsLockedOut ? "Account is locked out." : "Invalid password.";

                return new LoginResult
                {
                    Succeeded = false,
                    ErrorMessage = errorMessage
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);

            httpContext.Session.SetString("UserId", user.Id);
            httpContext.Session.SetString("Email", user.Email);
            httpContext.Session.SetString("Roles", string.Join(",", roles));

            return new LoginResult
            {
                Succeeded = true
            };
        }
        #endregion

        #region Register
        public async Task<IdentityResult> Register(RegisterRequest request, HttpContext httpContext)
        {
            var user = new User { UserName = request.Email, Email = request.Email, GenderEnum = GenderEnums.Male, StatusEnum = UserStatusEnums.Active };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleEnums.Customer.ToString());

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = GenerateEmailConfirmationLink(token, user.Email, httpContext);
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");
            }

            return result;
        }

        public async Task<bool> ConfirmEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }

        private string GenerateEmailConfirmationLink(string token, string email, HttpContext httpContext)
        {
            var request = httpContext.Request;
            var scheme = request.Scheme;
            var host = request.Host;
            var path = "/Auth/ConfirmEmail";

            return $"{scheme}://{host}{path}?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";
        }

        public async Task<bool> ResendConfirmationEmailAsync(string email, HttpContext httpContext)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || await _userManager.IsEmailConfirmedAsync(user))
            {
                return false;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = GenerateEmailConfirmationLink(token, user.Email, httpContext);
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

            return true;
        }
        #endregion
    }
}
