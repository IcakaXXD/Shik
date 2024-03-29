﻿using Microsoft.EntityFrameworkCore;
using Business_Layer;
using System;

namespace DataLayer
{
    public class ShikDBContext:DbContext
    {
        public ShikDBContext()
        {
                
        }
        public ShikDBContext(DbContextOptions contextOptions):base(contextOptions)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-PTKBD3O\\SQLEXPRESS01;Database=ShikDb;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<string>();
            modelBuilder.Entity<Clothes>().Property(c=>c.Size).HasConversion<string>();
            modelBuilder.Entity<Clothes>().Property(c=>c.ClotheType).HasConversion<string>();
            modelBuilder.Entity<Shipping>().Property(s=>s.Shipping_Method).HasConversion<string>();
            modelBuilder.Entity<OrderClothes>().HasKey(oc => new { oc.OrderId, oc.ClothesId });
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderClothes> OrdersClothes { get;set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        
    }
}
