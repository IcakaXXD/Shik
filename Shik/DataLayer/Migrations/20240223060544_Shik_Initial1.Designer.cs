﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(ShikDBContext))]
    [Migration("20240223060544_Shik_Initial1")]
    partial class Shik_Initial1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Business_Layer.Clothes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClotheType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("Business_Layer.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Discount_amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Exparation_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Business_Layer.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ClothesId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClothesId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Business_Layer.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ShippingId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ShippingId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Business_Layer.OrderClothes", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ClothesId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ClothesId");

                    b.HasIndex("ClothesId");

                    b.ToTable("OrdersClothes");
                });

            modelBuilder.Entity("Business_Layer.Shipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Shipping_Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Shipping_cost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Shipping");
                });

            modelBuilder.Entity("Business_Layer.Coupon", b =>
                {
                    b.HasOne("Business_Layer.Order", "Order")
                        .WithMany("Coupons")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Business_Layer.Customer", b =>
                {
                    b.HasOne("Business_Layer.Clothes", null)
                        .WithMany("Customers")
                        .HasForeignKey("ClothesId");
                });

            modelBuilder.Entity("Business_Layer.Order", b =>
                {
                    b.HasOne("Business_Layer.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Business_Layer.Shipping", "Shipping")
                        .WithMany("Orders")
                        .HasForeignKey("ShippingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("Business_Layer.OrderClothes", b =>
                {
                    b.HasOne("Business_Layer.Clothes", "Clothes")
                        .WithMany("OrderClothes")
                        .HasForeignKey("ClothesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Business_Layer.Order", "Order")
                        .WithMany("OrderClothes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothes");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Business_Layer.Clothes", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("OrderClothes");
                });

            modelBuilder.Entity("Business_Layer.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Business_Layer.Order", b =>
                {
                    b.Navigation("Coupons");

                    b.Navigation("OrderClothes");
                });

            modelBuilder.Entity("Business_Layer.Shipping", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
