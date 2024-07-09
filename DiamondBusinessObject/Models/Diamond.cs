﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

[Table("Diamond")]
public partial class Diamond
{
    [Key]
    public int DiamondId { get; set; }

    [StringLength(255)]
    public int? DiamondTypeId { get; set; }

    [StringLength(255)]
    public string DiamondName { get; set; }

    public float DiamondPrice { get; set; }

    public int? ImageId { get; set; }

    public bool? IsSold { get; set; }

    public double DiamondDiameter { get; set; }

    public double DiamondWeight { get; set; }

    public int DiamondColorId { get; set; }

    public int DiamondClarityId { get; set; }

    public int DiamondCutId { get; set; }

    [StringLength(255)]
    public string DiamondCertificate { get; set; }

    [InverseProperty("Diamond")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("DiamondTypeId")]
    [InverseProperty("Diamonds")]
    public virtual DiamondType DiamondType { get; set; }

    [ForeignKey("DiamondColorId")]
    [InverseProperty("Diamonds")]
    public virtual DiamondColor DiamondColor { get; set; }

    [ForeignKey("DiamondCutId")]
    [InverseProperty("Diamonds")]
    public virtual DiamondCut DiamondCut { get; set; }

    [ForeignKey("DiamondClarityId")]
    [InverseProperty("Diamonds")]
    public virtual DiamondClarity DiamondClarity { get; set; }

    [InverseProperty("Diamond")]
    public virtual ICollection<PaymentDiamond> PaymentDiamonds { get; set; } = new List<PaymentDiamond>();

    [InverseProperty("Diamond")]
    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();

    [InverseProperty("Diamond")]
    public virtual ICollection<MainDiamond> MainDiamonds { get; set; } = new List<MainDiamond>();

    [InverseProperty("Diamond")]
    public virtual ICollection<SecondaryDiamond> SecondaryDiamonds { get; set; } = new List<SecondaryDiamond>();

    [ForeignKey("ImageId")]
    public virtual Image Image { get; set; }
}