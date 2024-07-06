using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Auth
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly IAuthService _authService;

        public ConfirmEmailModel(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Error");
            }

            var result = await _authService.ConfirmEmailAsync(token, email);

            if (result)
            {
                TempData["ConfirmationSuccess"] = "Your email has been successfully confirmed.";
                return RedirectToPage("/Login");
            }
            else
            {
                // Xác nhận thất bại
                return RedirectToPage("/Error");
            }
        }
    }
}
