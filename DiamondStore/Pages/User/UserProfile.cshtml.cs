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
        private readonly IFirebaseService _firebaseService;

        public UserProfileModel(IUserService userService, IFirebaseService firebaseService)
        {
            _userService = userService;
            _firebaseService = firebaseService;
        }

        [BindProperty]
        public UserProfileViewDTO UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Auth/Login");
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

        public async Task<IActionResult> OnPostUploadAvatarAsync(IFormFile avatarImage)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Auth/Login");
            }

            if (avatarImage == null || avatarImage.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select a file to upload.");
                return Page();
            }

            var fileName = $"{Guid.NewGuid()}_{avatarImage.FileName}";

            using (var stream = new MemoryStream())
            {
                await avatarImage.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                try
                {
                    var imageUrl = await _firebaseService.UploadImageAsync(fileName, stream);

                    var result = await _userService.UpdateUserAvatarAsync(userId, imageUrl);

                    if (!result.Success)
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        return Page();
                    }

                    TempData["SuccessMessage"] = "Avatar updated successfully.";
                    return RedirectToPage("/User/UserProfile");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error uploading avatar: {ex.Message}");
                    return Page();
                }
            }
        }
    }
}
