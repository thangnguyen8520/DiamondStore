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

        public float LaborCost { get; set; }

        public float JewelryPrice { get; set; }

        public int JewelryInventory { get; set; }

        [StringLength(255)]
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public float TotalPrice { get; set; }


        [ForeignKey("JewelryTypeId")]
        [InverseProperty("Jewelries")]
        public virtual JewelryType JewelryType { get; set; }

        [ForeignKey("JewelryMaterialId")]
        [InverseProperty("Jewelries")]
        public virtual JewelryMaterial JewelryMaterial { get; set; }

        [ForeignKey("MainDiamondId")]
        [InverseProperty("Jewelries")]
        public virtual Diamond Diamond { get; set; }

        [InverseProperty("Jewelry")]
        public virtual ICollection<SecondaryDiamond> SecondaryDiamonds { get; set; } = new List<SecondaryDiamond>();

        [NotMapped]
        public string SecondaryDiamondsNames => string.Join(", ", SecondaryDiamonds.Select(sd => sd.Diamond?.DiamondName ?? "N/A"));

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        [InverseProperty("Jewelry")]
        public virtual ICollection<CartJewelry> CartJewelries { get; set; } = new List<CartJewelry>();
    }
}
