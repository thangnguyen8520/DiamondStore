using DiamondBusinessObject.DTO;
using DiamondBusinessObject.Models;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStore.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IPromotionService _promotionService;
        private readonly IUserService _userService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IPaymentService _paymentService;

        public CheckoutModel(ICartService cartService, IPromotionService promotionService, IUserService userService, IPaymentMethodService paymentMethodService, IPaymentService paymentService)
        {
            _cartService = cartService;
            _promotionService = promotionService;
            _userService = userService;
            _paymentMethodService = paymentMethodService;
            _paymentService = paymentService;
        }

        public float ProductTotal { get; set; }
        public float PromotionDiscount { get; set; }
        public float TotalPrice { get; set; }

        public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

        [BindProperty]
        public UserProfileViewDTO UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Auth/Login");
            }

            UserProfile = await _userService.GetUserByIdAsync(userId);
            UserProfile.UserId = userId;

            var cart = await _cartService.GetActiveCartByUserId(userId);
            if (cart == null)
            {
                return RedirectToPage("/Cart"); // Redirect to cart page if no active cart found
            }

            var cartPromotions = await _cartService.GetCartPromotions(userId);
            var userPromotions = await _promotionService.GetUserPromotions(userId);

            ProductTotal = cart.TotalPrice;
            PromotionDiscount = 0;

            foreach (var item in cart.CartDiamonds.Concat<object>(cart.CartJewelries))
            {
                foreach (var promotion in cartPromotions)
                {
                    var appliedPromotion = userPromotions.FirstOrDefault(up => up.PromotionId == promotion.UserPromotion.PromotionId);
                    if (appliedPromotion != null)
                    {
                        if (item is CartDiamond diamondItem)
                        {
                            PromotionDiscount += (float)(diamondItem.Diamond.DiamondPrice * diamondItem.Quantity * appliedPromotion.Promotion.DiscountRate / 100.0);
                        }
                        else if (item is CartJewelry jewelryItem)
                        {
                            PromotionDiscount += (float)(jewelryItem.Jewelry.TotalPrice * jewelryItem.Quantity * appliedPromotion.Promotion.DiscountRate / 100.0);
                        }
                    }
                }
            }

            TotalPrice = ProductTotal - PromotionDiscount;

            var paymentMethodsResult = await _paymentMethodService.GetPaymentMethodsAsync();
            if (paymentMethodsResult != null)
            {
                PaymentMethods = (List<PaymentMethod>)paymentMethodsResult;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int[] paymentMethods)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Auth/Login");
            }

            if (!paymentMethods.Any())
            {
                ModelState.AddModelError(string.Empty, "Please select a payment method.");
                return RedirectToPage("/Checkout");
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    var errors = value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            var addPaymentDTO = new AddPaymentDTO
            {
                FirstName = UserProfile.FirstName,
                LastName = UserProfile.LastName,
                Email = UserProfile.Email,
                Phone = UserProfile.PhoneNumber,
                Address = UserProfile.Address,
                PaymentMethodId = paymentMethods.First()
            };

            var (paymentLink, paymentId) = await _paymentService.ProcessPayment(userId, addPaymentDTO);

            if (!string.IsNullOrEmpty(paymentLink))
            {
                HttpContext.Session.SetInt32("PaymentId", paymentId);
                return Redirect(paymentLink);
            }

            ModelState.AddModelError(string.Empty, "Payment processing failed.");
            return Page();
        }

        public async Task<IActionResult> OnGetPaymentCallbackAsync(bool success, int paymentId)
        {
            if (paymentId > 0)
            {
                if (success)
                {
                    await _paymentService.HandlePaymentSuccessAsync(paymentId);
                    return RedirectToPage("/Index");
                }
                else
                {
                    await _paymentService.HandlePaymentCancellationAsync(paymentId);
                    return RedirectToPage("/Cart");
                }

                HttpContext.Session.Remove("PaymentId");
            }

            return RedirectToPage("/Index");
        }
    }
}
