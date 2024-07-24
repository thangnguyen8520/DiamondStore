﻿using System;
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
    }
}
