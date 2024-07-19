using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("CartJewelry")]
    public partial class CartJewelry
    {
        [Key]
        public int CartJewelryId { get; set; }

        public int CartId { get; set; }

        public int JewelryId { get; set; }

        public int JewelrySizeId { get; set; }

        public int Quantity { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime AddDate { get; set; }

        [ForeignKey("JewelryId")]
        public virtual Jewelry Jewelry { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("JewelrySizeId")]
        [InverseProperty("CartJewelries")]
        public virtual JewelrySize JewelrySize { get; set; }
    }
}
