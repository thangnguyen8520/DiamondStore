﻿// <auto-generated />
using System;
using DiamondBusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiamondDAO.Migrations
{
    [DbContext(typeof(DiamondStoreContext))]
    partial class DiamondStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiamondCertificate")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("DiamondClarityId")
                        .HasColumnType("int");

                    b.Property<int>("DiamondColorId")
                        .HasColumnType("int");

                    b.Property<int>("DiamondCutId")
                        .HasColumnType("int");

                    b.Property<double>("DiamondDiameter")
                        .HasColumnType("float");

                    b.Property<string>("DiamondName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("DiamondPrice")
                        .HasColumnType("real");

                    b.Property<int?>("DiamondTypeId")
                        .HasMaxLength(255)
                        .HasColumnType("int");

                    b.Property<double>("DiamondWeight")
                        .HasColumnType("float");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsSold")
                        .HasColumnType("bit");

                    b.HasKey("DiamondId");

                    b.HasIndex("DiamondClarityId");

                    b.HasIndex("DiamondColorId");

                    b.HasIndex("DiamondCutId");

                    b.HasIndex("DiamondTypeId");

                    b.HasIndex("ImageId");

                    b.ToTable("Diamond");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondClarity", b =>
                {
                    b.Property<int>("DiamondClarityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiamondClarityId"));

                    b.Property<string>("DiamondClarityName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DiamondClarityId");

                    b.ToTable("DiamondClarity");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondColor", b =>
                {
                    b.Property<int>("DiamondColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiamondColorId"));

                    b.Property<string>("DiamondColorName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DiamondColorId");

                    b.ToTable("DiamondColor");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondCut", b =>
                {
                    b.Property<int>("DiamondCutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiamondCutId"));

                    b.Property<string>("DiamondCutName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("DiamondCutId");

                    b.ToTable("DiamondCut");
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

            modelBuilder.Entity("DiamondBusinessObject.Models.Jewelry", b =>
                {
                    b.Property<int>("JewelryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JewelryId"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("JewelryMaterialId")
                        .HasColumnType("int");

                    b.Property<string>("JewelryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("JewelryPrice")
                        .HasColumnType("real");

                    b.Property<int>("JewelrySizeId")
                        .HasColumnType("int");

                    b.Property<int>("JewelryTypeId")
                        .HasColumnType("int");

                    b.Property<int>("JewelrydTypeId")
                        .HasColumnType("int");

                    b.Property<float>("LaborCost")
                        .HasColumnType("real");

                    b.Property<int>("MainDiamondId")
                        .HasColumnType("int");

                    b.Property<int?>("SecondaryDiamondId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("JewelryId");

                    b.HasIndex("ImageId");

                    b.HasIndex("JewelryMaterialId");

                    b.HasIndex("JewelrySizeId");

                    b.HasIndex("JewelrydTypeId");

                    b.ToTable("Jewelry");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.JewelryMaterial", b =>
                {
                    b.Property<int>("JewelryMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JewelryMaterialId"));

                    b.Property<string>("JewelryMaterialName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("JewelryMaterialId");

                    b.ToTable("JewelryMaterial");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.JewelrySize", b =>
                {
                    b.Property<int>("JewelrySizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JewelrySizeId"));

                    b.Property<string>("JewelrySizeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("JewelrySizeId");

                    b.ToTable("JewelrySize");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.JewelryType", b =>
                {
                    b.Property<int>("JewelryTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JewelryTypeId"));

                    b.Property<string>("JewelryTypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("JewelryTypeId");

                    b.ToTable("JewelryType");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.MainDiamond", b =>
                {
                    b.Property<int>("MainDiamondId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MainDiamondId"));

                    b.Property<int>("DiamondId")
                        .HasColumnType("int");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.HasKey("MainDiamondId");

                    b.HasIndex("DiamondId");

                    b.HasIndex("JewelryId");

                    b.ToTable("MainDiamond");
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

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("Id");

                    b.HasIndex("PaymentMethodId");

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

            modelBuilder.Entity("DiamondBusinessObject.Models.SecondaryDiamond", b =>
                {
                    b.Property<int>("SecondaryDiamondId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SecondaryDiamondId"));

                    b.Property<int>("DiamondId")
                        .HasColumnType("int");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.HasKey("SecondaryDiamondId");

                    b.HasIndex("DiamondId");

                    b.HasIndex("JewelryId");

                    b.ToTable("SecondaryDiamond");
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

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

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

                    b.ToTable("User", (string)null);
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

                    b.ToTable("Role", (string)null);
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

                    b.ToTable("RoleClaim", (string)null);
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

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
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
                    b.HasOne("DiamondBusinessObject.Models.DiamondClarity", "DiamondClarity")
                        .WithMany("Diamonds")
                        .HasForeignKey("DiamondClarityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.DiamondColor", "DiamondColor")
                        .WithMany("Diamonds")
                        .HasForeignKey("DiamondColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.DiamondCut", "DiamondCut")
                        .WithMany("Diamonds")
                        .HasForeignKey("DiamondCutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.DiamondType", "DiamondType")
                        .WithMany("Diamonds")
                        .HasForeignKey("DiamondTypeId");

                    b.HasOne("DiamondBusinessObject.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("DiamondClarity");

                    b.Navigation("DiamondColor");

                    b.Navigation("DiamondCut");

                    b.Navigation("DiamondType");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Jewelry", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("DiamondBusinessObject.Models.JewelryMaterial", "JewelryMaterial")
                        .WithMany("Jewelries")
                        .HasForeignKey("JewelryMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.JewelrySize", "JewelrySize")
                        .WithMany("Jewelries")
                        .HasForeignKey("JewelrySizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.JewelryType", "JewelryType")
                        .WithMany("Jewelries")
                        .HasForeignKey("JewelrydTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("JewelryMaterial");

                    b.Navigation("JewelrySize");

                    b.Navigation("JewelryType");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.MainDiamond", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Diamond", "Diamond")
                        .WithMany("MainDiamonds")
                        .HasForeignKey("DiamondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.Jewelry", "Jewelry")
                        .WithMany("s")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diamond");

                    b.Navigation("Jewelry");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Payment", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("Id");

                    b.HasOne("DiamondBusinessObject.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId");

                    b.Navigation("PaymentMethod");

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

            modelBuilder.Entity("DiamondBusinessObject.Models.SecondaryDiamond", b =>
                {
                    b.HasOne("DiamondBusinessObject.Models.Diamond", "Diamond")
                        .WithMany("SecondaryDiamonds")
                        .HasForeignKey("DiamondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiamondBusinessObject.Models.Jewelry", "Jewelry")
                        .WithMany("SecondaryDiamonds")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diamond");

                    b.Navigation("Jewelry");
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

                    b.Navigation("MainDiamonds");

                    b.Navigation("PaymentDiamonds");

                    b.Navigation("SecondaryDiamonds");

                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondClarity", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondColor", b =>
                {
                    b.Navigation("Diamonds");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.DiamondCut", b =>
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

            modelBuilder.Entity("DiamondBusinessObject.Models.Jewelry", b =>
                {
                    b.Navigation("SecondaryDiamonds");

                    b.Navigation("s");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.JewelryMaterial", b =>
                {
                    b.Navigation("Jewelries");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.JewelrySize", b =>
                {
                    b.Navigation("Jewelries");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.JewelryType", b =>
                {
                    b.Navigation("Jewelries");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.Payment", b =>
                {
                    b.Navigation("PaymentDiamonds");

                    b.Navigation("PaymentPromotions");
                });

            modelBuilder.Entity("DiamondBusinessObject.Models.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
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
