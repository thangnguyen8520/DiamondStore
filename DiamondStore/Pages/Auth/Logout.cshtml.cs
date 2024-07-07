using DiamondBusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("CurrentUser");
            HttpContext.Session.Remove("Roles");

            return RedirectToPage("/Index");
        }
    }
}
