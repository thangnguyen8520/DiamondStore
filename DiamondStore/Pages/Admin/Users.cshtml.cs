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

        public IList<UserDTO> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = (await _userService.GetAllActiveUsersAsync()).ToList();
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
