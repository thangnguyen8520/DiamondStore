using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Admin
{
    public class UserDeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public UserDeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { success = false, message = "Invalid user ID." });
            }

            var result = await _userService.UpdateUserStatusAsync(id, false); // Assuming false means Blocked

            if (result)
            {
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false });
        }
    }
}
