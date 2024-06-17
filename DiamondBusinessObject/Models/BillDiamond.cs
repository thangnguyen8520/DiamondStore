﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

[Table("BillDiamond")]
public partial class BillDiamond
{
    [Key]
    public int BillDiamondId { get; set; }

    public int? BillId { get; set; }

    public int? DiamondId { get; set; }

    [ForeignKey("BillId")]
    [InverseProperty("BillDiamonds")]
    public virtual Bill Bill { get; set; }

    [ForeignKey("DiamondId")]
    [InverseProperty("BillDiamonds")]
    public virtual Diamond Diamond { get; set; }
}