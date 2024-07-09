using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("DiamondColor")]
    public partial class DiamondColor
    {
        [Key]
        public int DiamondColorId { get; set; }

        [StringLength(255)]
        public string DiamondColorName { get; set; }

        [InverseProperty("DiamondColor")]
        public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();
    }
}
