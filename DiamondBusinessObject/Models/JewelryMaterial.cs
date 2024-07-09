using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("JewelryMaterial")]
    public partial class JewelryMaterial
    {
        [Key]
        public int JewelryMaterialId { get; set; }

        [StringLength(255)]
        public string JewelryMaterialName { get; set; }

        [InverseProperty("JewelryMaterial")]
        public virtual ICollection<Jewelry> Jewelries { get; set; } = new List<Jewelry>();
    }
}
