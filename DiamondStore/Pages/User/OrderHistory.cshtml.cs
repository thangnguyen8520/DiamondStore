using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.User
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IUserService _userService;

        public OrderHistoryModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<OrderHistoryDTO> OrderHistory { get; set; }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //    var userId = HttpContext.Session.GetString("UserId");
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return RedirectToPage("/Login");
        //    }

        //    OrderHistory = await _userService.GetOrderHistoryAsync(userId);

        //    return Page();
        //}

        //public async Task<IActionResult> OnGetOrderDetailsAsync(int id)
        //{
        //    var orderDetails = await _userService.GetOrderDetailsAsync(id);
        //    if (orderDetails == null)
        //    {
        //        return NotFound();
        //    }
        //    return new JsonResult(orderDetails);
        //}
    }
}
