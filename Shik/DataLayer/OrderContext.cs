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
                List<Clothes> clothes = new List<Clothes>(item.Clothes.Count);

                foreach (Clothes c in clothes)
                {
                    Clothes clothesFromDb = await dbContext.Clothes.FindAsync(c.Id);

                    if (clothesFromDb != null)
                    {
                        clothes.Add(clothesFromDb);
                    }
                    else
                    {
                        clothes.Add(c);
                    }
                }

                item.Clothes = clothes;
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
                        .Include(o => o.Clothes);
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
                        .Include(o => o.Clothes);
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

                    List<Clothes> clothes = new List<Clothes>(item.Clothes.Count);

                    foreach (Clothes c in clothes)
                    {
                        Clothes clothesFromDb = await dbContext.Clothes.FindAsync(c.Id);

                        if (clothesFromDb != null)
                        {
                            clothes.Add(clothesFromDb);
                        }
                        else
                        {
                            clothes.Add(c);
                        }
                    }

                    orderFromDb.Clothes = clothes;
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
