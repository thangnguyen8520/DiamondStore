using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages
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
                var result = await _authService.Register(SignupInput, HttpContext);

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
    }
}
