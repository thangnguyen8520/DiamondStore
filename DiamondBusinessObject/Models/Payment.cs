﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

[Table("Payment")]
public partial class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public int? CartId { get; set; }

    public string? UserId { get; set; }

    public int? PaymentMethodId { get; set; }

    [StringLength(255)]
    public string FullName { get; set; }

    [StringLength(255)]
    public string PhoneNumber { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(255)]
    public string Address { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(255)]
    public string ProductName { get; set; }

    public double? TotalAmount { get; set; }

    [StringLength(255)]
    public string Status { get; set; }

    [StringLength(255)]
    public string PaymentLink { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Payments")]
    public virtual PaymentMethod PaymentMethod { get; set; }
    [InverseProperty("Payment")]
    public virtual ICollection<PaymentDiamond> PaymentDiamonds { get; set; } = new List<PaymentDiamond>();

    [ForeignKey("UserId")]
    [InverseProperty("Payments")]
    public virtual User User { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("Payments")]
    public virtual Cart Cart { get; set; }
}