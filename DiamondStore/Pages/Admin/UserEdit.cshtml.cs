using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Admin
{
    public class UserEditModel : PageModel
    {
        private readonly IUserService _userService;

        public UserEditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserDTO User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User = await _userService.GetUserInAdminByIdAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.UpdateUserAsync(User);

            if (!result)
            {
                return Page();
            }

            return RedirectToPage("./Users");
        }
    }

}
