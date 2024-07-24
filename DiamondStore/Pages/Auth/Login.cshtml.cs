using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DiamondBusinessObject.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;
using DiamondBusinessObject.Models;
using Microsoft.AspNetCore.Http;

namespace DiamondStore.Pages.Auth
{
    public class LoginRegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginRegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginRequest LoginInput { get; set; }

        public void OnGet()
        {
        }

        #region Login By Email And Password
        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(LoginInput);

                if (result.Success)
                {
                    HttpContext.Session.SetString("UserId", result.UserId);
                    HttpContext.Session.SetString("Email", result.Email);
                    HttpContext.Session.SetString("Roles", string.Join(",", result.Roles));

                    var userId = HttpContext.Session.GetString("UserId");
                    var role = HttpContext.Session.GetString("Roles");

                    if ((role.Equals("Admin") || role.Equals("Staff")))
                    {
                        return Redirect("/Admin/Dashboard");
                    }

                    if (string.IsNullOrEmpty(userId) || role.Equals("Customer"))
                    {
                        return RedirectToPage("/Index");
                    }
                }

                if (result.ErrorMessage == "Email not confirmed.")
                {
                    TempData["ResendEmail"] = LoginInput.Email;
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostResendConfirmationAsync(string email)
        {
            var success = await _authService.ResendConfirmationEmailAsync(email);

            if (success)
            {
                TempData["ConfirmationSuccess"] = "Confirmation email has been resent. Please check your inbox.";
            }
            else
            {
                TempData["ConfirmationSuccess"] = "Unable to resend confirmation email. Please try again later.";
            }

            return RedirectToPage("/Login");
        }
        #endregion

        #region Google Login
        public IActionResult OnGetGoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("/Auth/Login", pageHandler: "GoogleCallback")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleCallbackAsync()
        {
            var result = await _authService.HandleGoogleCallbackAndSaveAsync();

            if (result.Success)
            {
                HttpContext.Session.SetString("UserId", result.UserId);
                HttpContext.Session.SetString("Email", result.Email);
                HttpContext.Session.SetString("Roles", string.Join(",", result.Roles));

                var userId = HttpContext.Session.GetString("UserId");
                var role = HttpContext.Session.GetString("Roles");

                if ((role.Equals("Admin") || role.Equals("Staff")))
                {
                    return Redirect("/Admin/Dashboard");
                }


                return RedirectToPage("/Index");
            }
            else
            {
                TempData["GoogleLoginError"] = result.ErrorMessage;
                return RedirectToPage("/Auth/Login");
            }
        }
        #endregion
    }

}
