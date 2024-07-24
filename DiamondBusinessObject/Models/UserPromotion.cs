using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("UserPromotion")]
    public partial class UserPromotion
    {
        [Key]
        public int UserPromotionId { get; set; }

        public string UserId { get; set; }

        public int PromotionId { get; set; }

        public int PromotionQuantity { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ExpiredDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PromotionId")]
        public virtual Promotion Promotion { get; set; }

        [InverseProperty("UserPromotion")]
        public virtual ICollection<CartPromotion> CartPromotions { get; set; } = new List<CartPromotion>();
    }
}
