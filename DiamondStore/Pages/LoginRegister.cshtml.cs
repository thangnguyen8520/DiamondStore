using DiamondStoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages
{
    public class LoginRegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        public void OnGet()
        {
        }
    }
}
