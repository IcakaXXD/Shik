using Business_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ShippingContext : IDB<Shipping, int>
    {
        private readonly ShikDBContext dbContext;
        public ShippingContext(ShikDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        public async Task CreateAsync(Shipping item)
        {
            try
            {
                dbContext.Shipping.Add(item);
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
                Shipping shippingFromDb = await ReadAsync(key, false, false);
                if (shippingFromDb == null)
                {
                    dbContext.Shipping.Remove(shippingFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("This shipping does not excist!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<Shipping>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Shipping> query = dbContext.Shipping;
                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Orders);
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

        public async Task<Shipping> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Shipping> query = dbContext.Shipping;
                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Orders);
                }
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.FirstOrDefaultAsync(c => c.Id == key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Shipping item, bool useNavigationalProperties = false)
        {
            try
            {
                Shipping shippingFromDb = await ReadAsync(item.Id, useNavigationalProperties);
                if (shippingFromDb == null)
                {
                    await CreateAsync(item);
                    return;
                }
                shippingFromDb.Shipping_Method = item.Shipping_Method;
                shippingFromDb.Shipping_cost = item.Shipping_cost;
                shippingFromDb.Orders= item.Orders;
                shippingFromDb.DeliveryTime = item.DeliveryTime;
                if (useNavigationalProperties)
                {
                    List<Order> orders = new List<Order>();
                    foreach (Order o in item.Orders)
                    {
                        Order orderFromDb = dbContext.Orders.Find(o.Id);
                        if (orderFromDb != null)
                        {
                            orders.Add(orderFromDb);
                        }
                        else
                        {
                            orders.Add(o);
                        }
                    }
                    shippingFromDb.Orders = orders;
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
