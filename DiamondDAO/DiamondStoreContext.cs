﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiamondBusinessObject.Models;

public partial class DiamondStoreContext : IdentityDbContext<User>
{
    public DiamondStoreContext()
    {
    }

    public DiamondStoreContext(DbContextOptions<DiamondStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Diamond> Diamonds { get; set; }

    public virtual DbSet<DiamondType> DiamondTypes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentDiamond> PaymentDiamonds { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentPromotion> PaymentPromotions { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity => entity.ToTable("User"));
        builder.Entity<IdentityRole>(entity => entity.ToTable("Role"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRole"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaim"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogin"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserToken"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaim"));
    }
}