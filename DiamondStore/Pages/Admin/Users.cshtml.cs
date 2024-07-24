using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<UserDTO> Users { get; private set; }

        public async Task OnGetAsync()
        {
            Users = (await _userService.GetAllActiveUsersAsync()).ToList();
        }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Partial("_UserDetailPartial", user);
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Partial("_UserEditPartial", user);
        }

        public async Task<IActionResult> OnPostEditAsync(UserDTO userDTO)
        {
            await _userService.UpdateUserAsync(userDTO);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Partial("_UserDeletePartial", user);
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToPage();
        }
    }

}
