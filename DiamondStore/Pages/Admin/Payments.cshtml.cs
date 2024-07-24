using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Admin
{
    public class PaymentsModel : PageModel
    {
        private readonly IPaymentAdminService _paymentAdminService;

        public PaymentsModel(IPaymentAdminService paymentAdminService)
        {
            _paymentAdminService = paymentAdminService;
        }

        public IList<PaymentDTO> Payments { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userId) || (!role.Equals("Admin") && !role.Equals("Staff")))
            {
                return Redirect("/Auth/Login");
            }

            Payments = (await _paymentAdminService.GetAllPaymentsAsync()).ToList();

            return Page();
        }
    }
}
