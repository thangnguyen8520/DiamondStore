namespace DiamondStoreService.Models
{
    public class DiamondDTO
    {
        public int DiamondId { get; set; }
        public string DiamondName { get; set; }
        public float DiamondPrice { get; set; }
        public double DiamondWeight { get; set; }
        public string DiamondColorName { get; set; }
        public string DiamondClarityName { get; set; }
        public string DiamondCutName { get; set; }
        public string DiamondTypeName { get; set; }
        public double DiamondDiameter { get; set; }
        public string DiamondCertificate { get; set; }
        public int DiamondInventory { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
