using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondBusinessObject.Models
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
