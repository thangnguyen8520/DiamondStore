using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Models
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public DateTime OrderDate { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public string PaymentMethodName { get; set; }
        public double? TotalAmount { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
    }
}
