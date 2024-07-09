using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("JewelrySize")]
    public partial class JewelrySize
    {
        [Key]
        public int JewelrySizeId { get; set; }

        [StringLength(255)]
        public string JewelrySizeName { get; set; }

        [InverseProperty("JewelrySize")]
        public virtual ICollection<Jewelry> Jewelries { get; set; } = new List<Jewelry>();
    }
}
