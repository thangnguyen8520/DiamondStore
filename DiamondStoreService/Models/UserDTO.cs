using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Models
{
    public class UserDTO
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
    }
}
