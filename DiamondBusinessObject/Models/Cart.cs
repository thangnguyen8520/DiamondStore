﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

[Table("Cart")]
public partial class Cart
{
    [Key]
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int? DiamondId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [ForeignKey("DiamondId")]
    [InverseProperty("Carts")]
    public virtual Diamond Diamond { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Carts")]
    public virtual User User { get; set; }
}