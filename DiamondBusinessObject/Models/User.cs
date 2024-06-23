﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

public partial class User : IdentityUser
{
    [StringLength(255)]
    public string Address { get; set; }

    public bool? Gender { get; set; }

    public bool? Status { get; set; }

    public DateTime? LastLogin { get; set; }

    public int? ImageId { get; set; }

    [ForeignKey("ImageId")]
    public virtual Image Image { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

}