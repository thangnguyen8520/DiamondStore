using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterRequest SignupInput { get; set; }

        public void OnGet()
        {
        }
    }
}
