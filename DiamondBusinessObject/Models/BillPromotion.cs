﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

[Table("BillPromotion")]
public partial class BillPromotion
{
    [Key]
    public int BillPromotionId { get; set; }

    public int? BillId { get; set; }

    public int? PromotionId { get; set; }

    [ForeignKey("BillId")]
    [InverseProperty("BillPromotions")]
    public virtual Bill Bill { get; set; }

    [ForeignKey("PromotionId")]
    [InverseProperty("BillPromotions")]
    public virtual Promotion Promotion { get; set; }
}