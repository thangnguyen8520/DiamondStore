using Azure;
using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DiamondStore.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAuthService _authService;

        public ForgotPasswordModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Message { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("CurrentUser");
            HttpContext.Session.Remove("Roles");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _authService.SendForgotPasswordEmailAsync(Input.Email);
            if (result.Success)
            {
                Message = "Password reset email sent. Please check your ";
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Failed to send password reset email.");
            }

            return Page();
        }
    }
}
