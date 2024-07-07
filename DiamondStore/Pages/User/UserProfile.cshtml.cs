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
                UserId = user.UserId,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            var result = await _userService.UpdateUserProfileAsync(userId, UserProfile);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return Page();
            }

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToPage("/User/UserProfile");
        }
    }
}
