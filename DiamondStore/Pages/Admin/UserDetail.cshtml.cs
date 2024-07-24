using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Admin
{
    public class UserDetailModel : PageModel
    {
        private readonly IUserService _userService;

        public UserDetailModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserDTO User { get; private set; }

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
    }

}
