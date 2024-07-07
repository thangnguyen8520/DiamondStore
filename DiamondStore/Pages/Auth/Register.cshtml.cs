using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterRequest SignupInput { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSignupAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(SignupInput);

                if (result.Succeeded)
                {
                    TempData["RegistrationSuccess"] = "Registration successful!";
                    return Page();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

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
