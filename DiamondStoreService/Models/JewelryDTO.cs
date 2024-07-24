using System;
using System.Collections.Generic;

namespace DiamondStoreService.Models
{
    public class JewelryDTO
    {
        public int JewelryId { get; set; }
        public string JewelryName { get; set; }
        public float JewelryPrice { get; set; }
        public float TotalPrice { get; set; }
        public string JewelryMaterialName { get; set; }
        public string JewelryTypeName { get; set; }

        public DateTime? CreateDate { get; set; }
        public string Status { get; set; }
        public string MainDiamondName { get; set; }
        public List<string> SecondaryDiamondsNames { get; set; } = new List<string>();

        public int JewelryMaterialId { get; set; }
        public int JewelryTypeId { get; set; }
        public int MainDiamondId { get; set; }
        public float LaborCost { get; set; }
    }

    public class JewelryMaterialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class JewelryTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
