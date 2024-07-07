using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DiamondStore.Pages.User
{
    public class UserProfileModel : PageModel
    {
        private readonly IUserService _userService;

        public UserProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserProfileViewDTO UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            var user = await _userService.GetUserByIdAsync(userId); // Lấy thông tin người dùng
            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            UserProfile = new UserProfileViewDTO
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return Page();
        }
    }
}
