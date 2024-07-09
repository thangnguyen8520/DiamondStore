using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("DiamondCut")]
    public partial class DiamondCut
    {
        [Key]
        public int DiamondCutId { get; set; }

        [StringLength(255)]
        public string DiamondCutName { get; set; }

        [InverseProperty("DiamondCut")]
        public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();
    }
}
