using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DiamondStore.Pages
{
    public class LoginRegisterModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginRegisterModel(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public LoginRequest LoginInput { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(LoginInput, _httpContextAccessor.HttpContext);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
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
            var success = await _authService.ResendConfirmationEmailAsync(email, _httpContextAccessor.HttpContext);

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
    }

}
