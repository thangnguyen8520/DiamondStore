using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("Jewelry")]
    public partial class Jewelry
    {
        [Key]
        public int JewelryId { get; set; }

        [StringLength(255)]
        public string JewelryName { get; set; }

        public int? ImageId { get; set; }

        public int JewelryMaterialId { get; set; }

        public int JewelryTypeId { get; set; }

        public int MainDiamondId { get; set; }

        public int? SecondaryDiamondId { get; set; }

        public float LaborCost { get; set; }

        public float JewelryPrice { get; set; }

        [StringLength(255)]
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public float TotalPrice { get; set; }

        [InverseProperty("Jewelry")]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        [ForeignKey("JewelrydTypeId")]
        [InverseProperty("Jewelries")]
        public virtual JewelryType JewelryType { get; set; }

        [ForeignKey("JewelryMaterialId")]
        [InverseProperty("Jewelries")]
        public virtual JewelryMaterial JewelryMaterial { get; set; }

        [InverseProperty("Jewelry")]
        public virtual ICollection<SecondaryDiamond> SecondaryDiamonds { get; set; } = new List<SecondaryDiamond>();

        [InverseProperty("Jewelry")]
        public virtual ICollection<MainDiamond> MainDiamonds { get; set; } = new List<MainDiamond>();

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
    }
}
