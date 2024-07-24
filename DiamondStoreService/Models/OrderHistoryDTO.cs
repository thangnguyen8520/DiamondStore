namespace DiamondStoreService.Models
{
    public class OrderHistoryDTO
    {
        public int PaymentId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string FuName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal Cash { get; set; }
        public decimal BankTransfer { get; set; }
        public decimal Subtotal { get; set; }
        public string Discount { get; set; }
        public string ShippingFee { get; set; }
        public List<PaymentDiamondDTO> PaymentDiamonds { get; set; }
    }

    public class PaymentDiamondDTO
    {
        public int PaymentDiamondId { get; set; }
        public string DiamondName { get; set; }
        public double? CaratWeight { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
    }
}
