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
        public List<PaymentJewelryDTO> PaymentJewelries { get; set; } = new List<PaymentJewelryDTO>();

    }

    public class PaymentDiamondDTO
    {
        public string Type { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Total { get; set; }
    }

    public class PaymentJewelryDTO
    {
        public string Type { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Total { get; set; }
    }
}
