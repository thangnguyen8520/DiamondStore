using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("JewelryType")]
    public partial class JewelryType
    {
        [Key]
        public int JewelryTypeId { get; set; }

        [StringLength(255)]
        public string JewelryTypeName { get; set; }

        [InverseProperty("JewelryType")]
        public virtual ICollection<Jewelry> Jewelries { get; set; } = new List<Jewelry>();
    }
}
