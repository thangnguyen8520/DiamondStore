﻿// <auto-generated />
using System;
using DiamondBusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiamondDAO.Migrations
{
    [DbContext(typeof(DiamondStoreContext))]
    [Migration("20240623105820_InitialDB")]
    partial class InitialDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DiamondBusinessObject.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("DiamondId")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("DiamondId");

                    b.HasIndex("Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Diamond", b =>
                {
                    b.Property<int>("DiamondId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiamondId"));

                    b.Property<double?>("CaratWeight")
                        .HasColumnType("float");

                    b.Property<string>("Clarity")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Color")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Cut")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DiamondName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("DiamondPriceId")
                        .HasColumnType("int");

                    b.Property<int?>("DiamondTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsSold")
                        .HasColumnType("bit");

                    b.Property<string>("Origin")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DiamondId");

                    b.HasIndex("DiamondPriceId");

                    b.HasIndex("DiamondTypeId");

                    b.ToTable("Diamond");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondPrice", b =>
                {
                    b.Property<int>("DiamondPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiamondPriceId"));

                    b.Property<double?>("CaratWeight")
                        .HasColumnType("float");

                    b.Property<string>("Clarity")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Color")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Cut")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Origin")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("PricePerCarat")
                        .HasColumnType("float");

                    b.Property<DateOnly?>("UpdateDate")
                        .HasColumnType("date");

                    b.HasKey("DiamondPriceId");

                    b.ToTable("DiamondPrice");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondType", b =>
                {
                    b.Property<int>("DiamondTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiamondTypeId"));

                    b.Property<string>("DiamondTypeName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DiamondTypeId");

                    b.ToTable("DiamondType");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FullName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProductName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.PaymentDiamond", b =>
                {
                    b.Property<int>("PaymentDiamondId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentDiamondId"));

                    b.Property<int?>("DiamondId")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.HasKey("PaymentDiamondId");

                    b.HasIndex("DiamondId");

                    b.HasIndex("PaymentId");

                    b.ToTable("PaymentDiamond");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"));

                    b.Property<string>("PaymentMethodName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.PaymentPromotion", b =>
                {
                    b.Property<int>("PaymentPromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentPromotionId"));

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int?>("PromotionId")
                        .HasColumnType("int");

                    b.HasKey("PaymentPromotionId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("PromotionId");

                    b.ToTable("PaymentPromotion");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionId"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("DiscountRate")
                        .HasColumnType("float");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PromotionType")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("PromotionId");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Warranty", b =>
                {
                    b.Property<int>("WarrantyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyId"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("DiamondId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.HasKey("WarrantyId");

                    b.HasIndex("DiamondId");

                    b.ToTable("Warranty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Cart", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Diamond", "Diamond")
                        .WithMany("Carts")
                        .HasForeignKey("DiamondId");

                    b.HasOne("DiamondBusinessObject.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("Id");

                    b.Navigation("Diamond");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Diamond", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.DiamondPrice", "DiamondPrice")
                        .WithMany("Diamonds")
                        .HasForeignKey("DiamondPriceId");

                    b.HasOne("DiamondBusinessObject.Models.DiamondType", "DiamondType")
                        .WithMany("Diamonds")
                        .HasForeignKey("DiamondTypeId");

                    b.Navigation("DiamondPrice");

                    b.Navigation("DiamondType");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Payment", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("Id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.PaymentDiamond", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Diamond", "Diamond")
                        .WithMany("PaymentDiamonds")
                        .HasForeignKey("DiamondId");

                    b.HasOne("DiamondBusinessObject.Models.Payment", "Payment")
                        .WithMany("PaymentDiamonds")
                        .HasForeignKey("PaymentId");

                    b.Navigation("Diamond");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.PaymentPromotion", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Payment", "Payment")
                        .WithMany("PaymentPromotions")
                        .HasForeignKey("PaymentId");

                    b.HasOne("DiamondBusinessObject.Models.Promotion", "Promotion")
                        .WithMany("PaymentPromotions")
                        .HasForeignKey("PromotionId");

                    b.Navigation("Payment");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.User", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Image", "Image")
                        .WithMany("Users")
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Warranty", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Diamond", "Diamond")
                        .WithMany("Warranties")
                        .HasForeignKey("DiamondId");

                    b.Navigation("Diamond");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Diamond", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("PaymentDiamonds");

                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondPrice", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondType", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Image", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Payment", b =>
                {
                    b.Navigation("PaymentDiamonds");

                    b.Navigation("PaymentPromotions");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Promotion", b =>
                {
                    b.Navigation("PaymentPromotions");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
