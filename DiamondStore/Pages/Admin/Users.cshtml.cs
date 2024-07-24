using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStore.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<UserDTO> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }

            Users = (await _userService.GetAllActiveUsersAsync()).ToList();

            return Page();
        }

        //public async Task<IActionResult> OnGetDetailsAsync(string id)
        //{
        //    var user = await _userService.GetUserDetailAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return new JsonResult(user);
        //}
    }
}
