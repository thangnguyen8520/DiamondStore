﻿using System;
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

    public string UserId { get; set; }

    [NotMapped]
    public float TotalPrice
    {
        get
        {
            float price = 0;
            foreach (var cartDiamond in CartDiamonds)
            {
                price += cartDiamond.Diamond.DiamondPrice * cartDiamond.Quantity;
            }
            foreach (var cartJewelry in CartJewelries)
            {
                price += cartJewelry.Jewelry.TotalPrice * cartJewelry.Quantity;
            }
            return price;
        }
    }

    [NotMapped]
    public float DisplayTotalPrice { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    public bool Status { get; set; }


    [ForeignKey("UserId")]
    [InverseProperty("Carts")]
    public virtual User User { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartDiamond> CartDiamonds { get; set; } = new List<CartDiamond>();

    [InverseProperty("Cart")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Cart")]
    public virtual ICollection<CartJewelry> CartJewelries { get; set; } = new List<CartJewelry>();

    [InverseProperty("Cart")]
    public virtual ICollection<CartPromotion> CartPromotions { get; set; } = new List<CartPromotion>();


}
