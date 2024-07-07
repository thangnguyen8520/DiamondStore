using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Models
{
    public class UserProfileViewDTO
    {
        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        [Required]
        public string LastName { get; set; }

        [BindProperty]
        [Required]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required]
        public string Address { get; set; }

        [BindProperty]
        public string AvatarUrl { get; set; }
    }
}
