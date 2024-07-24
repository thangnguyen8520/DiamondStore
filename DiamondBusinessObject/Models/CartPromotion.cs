using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("CartPromotion")]
    public partial class CartPromotion
    {
        [Key]
        public int CartPromotionId {  get; set; }
        public int CartId { get; set; }

        public int UserPromotionId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("UserPromotionId")]
        public virtual UserPromotion UserPromotion { get; set; }
    }
}
