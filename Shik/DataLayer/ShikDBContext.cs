using Microsoft.EntityFrameworkCore;
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
            //optionsBuilder.UseSqlServer("Server=    ;Database=ShopDb11J;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderClothes>().HasKey(po => new { po.ClothesId, po.OrderId });
            //modelBuilder.Entity<OrderClothes>().HasOne(po => po.Order).WithMany(o => o.Clothes).HasForeignKey(po => po.OrderId);
            modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<string>();
            modelBuilder.Entity<Clothes>().Property(c=>c.Size).HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderClothes> OrdersClothes { get;set; }
        
    }
}
