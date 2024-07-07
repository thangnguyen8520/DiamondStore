using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DiamondStore.Pages.Auth
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAuthService _authService;

        public ResetPasswordModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Message { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Token { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string token, string email)
        {
            Input = new InputModel
            {
                Token = token,
                Email = email
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _authService.ResetPasswordAsync(Input.Email, Input.Token, Input.Password);
            if (result)
            {
                Message = "Your password has been reset. You can now log in.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to reset password.");
            }

            return Page();
        }
    }
}
