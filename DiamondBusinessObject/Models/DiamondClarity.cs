using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("DiamondClarity")]
    public partial class DiamondClarity
    {
        [Key]
        public int DiamondClarityId { get; set; }

        [StringLength(255)]
        public string DiamondClarityName { get; set; }

        [InverseProperty("DiamondClarity")]
        public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();
    }
}
