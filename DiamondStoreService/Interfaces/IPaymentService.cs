using DiamondBusinessObject.DTO;
using DiamondBusinessObject.Models;
﻿using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IPaymentService
    {
        Task<(string PaymentLink, int PaymentId)> ProcessPayment(string userId, AddPaymentDTO paymentDetails);
        Task HandlePaymentSuccessAsync(int paymentId);
        Task HandlePaymentCancellationAsync(int paymentId);
        Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync();
    }
}
