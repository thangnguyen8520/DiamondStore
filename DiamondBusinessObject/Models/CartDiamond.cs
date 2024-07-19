using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("CartDiamond")]
    public partial class CartDiamond
    {
        [Key]
        public int CartDiamondId { get; set; }

        public int CartId { get; set; }

        public int DiamondId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AddDate { get; set; }

        [ForeignKey("DiamondId")]
        public virtual Diamond Diamond { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
    }
}
