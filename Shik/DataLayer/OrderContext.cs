using Business_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderContext : IDB<Order, int>
    {
        private readonly ShikDBContext dbContext;

        public OrderContext(ShikDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(Order item)
        {
            try
            {
                Customer customerFromDb = await dbContext.Customers.FindAsync(item.CustomerId);

                if (customerFromDb != null)
                {
                    item.Customer = customerFromDb;
                }
                List<OrderClothes> clothes = new List<OrderClothes>(item.OrderClothes.Count);

                foreach (OrderClothes c in clothes)
                {
                    OrderClothes clothesFromDb = await dbContext.OrdersClothes.FindAsync(c.ClothesId);

                    if (clothesFromDb != null)
                    {
                        clothes.Add(clothesFromDb);
                    }
                    else
                    {
                        clothes.Add(c);
                    }
                }
                List<Coupon> coupons = new List<Coupon>(item.Coupons.Count);
                foreach (Coupon c in item.Coupons)
                {
                    Coupon couponFromDb = await dbContext.Coupons.FindAsync(c.Id);
                    if (couponFromDb!= null)
                    {
                        coupons.Add(couponFromDb);
                    }
                    else
                    {
                        coupons.Add(c);
                    }
                }
                item.Coupons = coupons;
                item.OrderClothes = clothes;
                dbContext.Orders.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Order orderFromDb = await ReadAsync(key, false, false);

                if (orderFromDb != null)
                {
                    dbContext.Orders.Remove(orderFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Order with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Order>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Order> query = dbContext.Orders;

                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Customer)
                        .Include(oc => oc.OrderClothes).ThenInclude(o=>o.Clothes)
                        .Include(c=>c.Coupons)
                        .Include(s => s.Shipping);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Order> query = dbContext.Orders;

                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Customer)
                        .Include(o => o.OrderClothes).ThenInclude (o=>o.Clothes)
                        .Include(c=>c.Coupons)
                        .Include(s=>s.Shipping);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(o => o.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Order item, bool useNavigationalProperties = false)
        {
            try
            {
                Order orderFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                orderFromDb.Price = item.Price;
                orderFromDb.Address = item.Address;
                orderFromDb.Date = item.Date;
                orderFromDb.Status = item.Status;
                orderFromDb.Customer = item.Customer;
                orderFromDb.Coupons = item.Coupons;
                orderFromDb.Shipping = item.Shipping;
                orderFromDb.OrderClothes = item.OrderClothes;

                if (useNavigationalProperties)
                {
                    Customer customerFromDb = await dbContext.Customers.FindAsync(item.CustomerId);

                    if (customerFromDb != null)
                    {
                        orderFromDb.Customer = customerFromDb;
                    }
                    else
                    {
                        orderFromDb.Customer = item.Customer;
                    }

                    Shipping shippingFromDb = await dbContext.Shipping.FindAsync(item.ShippingId);

                    if (shippingFromDb != null)
                    {
                        orderFromDb.Shipping = shippingFromDb;
                    }
                    else
                    {
                        orderFromDb.Shipping = item.Shipping;
                    }

                    List<OrderClothes> clothes = new List<OrderClothes>(item.OrderClothes.Count);

                    foreach (OrderClothes c in clothes)
                    {
                        OrderClothes clothesFromDb = await dbContext.OrdersClothes.FindAsync(c.ClothesId);

                        if (clothesFromDb != null)
                        {
                            clothes.Add(clothesFromDb);
                        }
                        else
                        {
                            clothes.Add(c);
                        }
                    }
                    List<Coupon> coupon = new List<Coupon>();
                    foreach (Coupon o in item.Coupons)
                    {
                        Coupon couponFromDb = dbContext.Coupons.Find(o.Id);
                        if (couponFromDb != null)
                        {
                            coupon.Add(couponFromDb);
                        }
                        else
                        {
                            coupon.Add(o);
                        }
                    }
                    orderFromDb.Coupons = coupon;
                    orderFromDb.OrderClothes = clothes;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
