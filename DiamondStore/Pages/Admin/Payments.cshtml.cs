using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiamondStore.Pages.Admin
{
    public class PaymentsModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public PaymentsModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public IList<PaymentDTO> Payments { get; private set; }

        public async Task OnGetAsync()
        {
            Payments = (await _paymentService.GetAllPaymentsAsync()).ToList();
        }
    }
}
