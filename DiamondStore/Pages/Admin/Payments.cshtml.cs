using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            Payments = (await _paymentAdminService.GetAllPaymentsAsync()).ToList();
        }

        public async Task<IActionResult> OnGetPaymentDetailsAsync(int id)
        {
            var paymentDetails = await _paymentAdminService.GetPaymentDetailsAsync(id);
            if (paymentDetails == null)
            {
                return NotFound();
            }
            return new JsonResult(paymentDetails);
        }
    }
}
