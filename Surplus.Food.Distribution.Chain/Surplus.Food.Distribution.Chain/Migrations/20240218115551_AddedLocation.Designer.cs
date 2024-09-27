﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Surplus.Food.Distribution.Chain.Data;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240218115551_AddedLocation")]
    partial class AddedLocation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.DeliveryPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DrivingLicense")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nic")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DeliveryPersons");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Donor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("DeletedFlag")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("FoodSupplierId")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("PriceStatusId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodSupplierId");

                    b.HasIndex("PriceStatusId")
                        .IsUnique();

                    b.HasIndex("ServiceTypeId")
                        .IsUnique();

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodSupplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BrNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OwnerNic")
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SuplierName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TrainingLicense")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FoodSuppliers");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodSupplierReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("FoodSupplierReviews");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PickupTime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PriceStatusId")
                        .HasColumnType("int");

                    b.Property<string>("RecieverName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("PriceStatusId")
                        .IsUnique();

                    b.HasIndex("ServiceTypeId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.OrderItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("FoodItemId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CardType")
                        .HasColumnType("longtext");

                    b.Property<string>("CardholderName")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DonerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ExpireDate")
                        .HasColumnType("longtext");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.RefOrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RefOrderStatuses");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.RefPriceStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PriceStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RefPriceStatuses");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.RefServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RefServiceTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Admin", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUserRole", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Customer", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", "User")
                        .WithMany("Customers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.DeliveryPerson", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", "User")
                        .WithMany("DeliveryPersons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Donor", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", "User")
                        .WithMany("Donors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodItem", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.FoodSupplier", "FoodSupplier")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodSupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.RefPriceStatus", "PriceStatus")
                        .WithOne("FoodItems")
                        .HasForeignKey("Surplus.Food.Distribution.Chain.Models.DbModels.FoodItem", "PriceStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.RefServiceType", "ServiceType")
                        .WithOne("FoodItems")
                        .HasForeignKey("Surplus.Food.Distribution.Chain.Models.DbModels.FoodItem", "ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodSupplier");

                    b.Navigation("PriceStatus");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodSupplier", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", "User")
                        .WithMany("FoodSuppliers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Order", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.RefOrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.RefPriceStatus", "PriceStatus")
                        .WithOne("Orders")
                        .HasForeignKey("Surplus.Food.Distribution.Chain.Models.DbModels.Order", "PriceStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.RefServiceType", "ServiceType")
                        .WithOne("Orders")
                        .HasForeignKey("Surplus.Food.Distribution.Chain.Models.DbModels.Order", "ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");

                    b.Navigation("PriceStatus");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.OrderItems", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.FoodItem", "FoodItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Payment", b =>
                {
                    b.HasOne("Surplus.Food.Distribution.Chain.Models.DbModels.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.ApplicationUser", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Customers");

                    b.Navigation("DeliveryPersons");

                    b.Navigation("Donors");

                    b.Navigation("FoodSuppliers");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.FoodSupplier", b =>
                {
                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.RefPriceStatus", b =>
                {
                    b.Navigation("FoodItems")
                        .IsRequired();

                    b.Navigation("Orders")
                        .IsRequired();
                });

            modelBuilder.Entity("Surplus.Food.Distribution.Chain.Models.DbModels.RefServiceType", b =>
                {
                    b.Navigation("FoodItems")
                        .IsRequired();

                    b.Navigation("Orders")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
