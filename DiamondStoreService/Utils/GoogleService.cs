using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiamondStoreService.Utils
{
    public class GoogleService : IGoogleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoogleService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GoogleCallbackResult> HandleGoogleCallbackAsync()
        {
            var authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return new GoogleCallbackResult { Success = false, ErrorMessage = "Authentication failed." };
            }

            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var firstName = authenticateResult.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "";
            var lastName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Surname) ?? "";
            var avatarUrl = authenticateResult.Principal.FindFirstValue("urn:google:picture");

            return new GoogleCallbackResult
            {
                Success = true,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                AvatarUrl = avatarUrl
            };
        }
    }
}
