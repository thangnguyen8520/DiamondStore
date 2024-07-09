using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("MainDiamond")]
    public partial class MainDiamond
    {
        [Key]
        public int MainDiamondId { get; set; }

        public int DiamondId { get; set; }

        public int JewelryId { get; set; }

        [ForeignKey("DiamondId")]
        public virtual Diamond Diamond { get; set; }

        [ForeignKey("JewelryId")]
        public virtual Jewelry Jewelry { get; set; }
    }
}
