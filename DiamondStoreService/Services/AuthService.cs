using AutoMapper;
using Azure.Core;
using DiamondBusinessObject.Enums;
using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly IGoogleService _googleService;

        public AuthService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IMemoryCache cache, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IEmailSender emailSender, 
            IGoogleService googleService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _googleService = googleService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Login By Email And Password
        public async Task<LoginResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "User not found."
                };
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Email not confirmed."
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!signInResult.Succeeded)
            {
                string errorMessage = signInResult.IsLockedOut ? "Account is locked out." : "Invalid password.";

                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new LoginResult
            {
                Success = true,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles
            };
        }
        #endregion

        #region Register
        public async Task<IdentityResult> Register(RegisterRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, GenderEnum = GenderEnums.Male, StatusEnum = UserStatusEnums.Active };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleEnums.Customer.ToString());

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = GenerateEmailConfirmationLink(token, user.Email);
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

        private string GenerateEmailConfirmationLink(string token, string email)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var scheme = request.Scheme;
            var host = request.Host;
            var path = "/Auth/ConfirmEmail";

            return $"{scheme}://{host}{path}?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";
        }

        public async Task<bool> ResendConfirmationEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || await _userManager.IsEmailConfirmedAsync(user))
            {
                return false;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = GenerateEmailConfirmationLink(token, user.Email);
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

            return true;
        }
        #endregion

        #region Google Login
        public async Task<LoginResult> HandleGoogleCallbackAndSaveAsync()
        {
            var googleCallbackResult = await _googleService.HandleGoogleCallbackAsync();

            if (!googleCallbackResult.Success)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = googleCallbackResult.ErrorMessage
                };
            }

            var email = googleCallbackResult.Email;
            var firstName = googleCallbackResult.FirstName;
            var lastName = googleCallbackResult.LastName;
            var avatarUrl = googleCallbackResult.AvatarUrl;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var avatarImage = new Image
                {
                    ImageUrl = avatarUrl
                };

                await _unitOfWork.ImageRepository.AddAsync(avatarImage);
                await _unitOfWork.SaveChangeAsync();

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    Email = email,
                    ImageId = avatarImage.Id,
                    GenderEnum = GenderEnums.Male,
                    StatusEnum = UserStatusEnums.Active
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return new LoginResult { Success = false, ErrorMessage = "Failed to create user." };
                }

                await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", user.Id, "Google"));
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            else
            {
                // Check if user has avatar already
                if (user.ImageId == null)
                {
                    var avatarImage = new Image
                    {
                        ImageUrl = avatarUrl
                    };

                    await _unitOfWork.ImageRepository.AddAsync(avatarImage);
                    await _unitOfWork.SaveChangeAsync();

                    user.ImageId = avatarImage.Id;
                }
                // else: User already has an avatar, do nothing

                await _userManager.UpdateAsync(user);
            }

            var roles = await _userManager.GetRolesAsync(user);

            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new LoginResult
            {
                Success = true,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles
            };
        }
        #endregion
    }
}
