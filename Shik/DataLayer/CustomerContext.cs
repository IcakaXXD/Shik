using Business_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CustomerContext:IDB<Customer,int>
    {
        private readonly ShikDBContext dbContext;
        public CustomerContext(ShikDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        public async Task CreateAsync(Customer item)
        {
            try
            {
                dbContext.Customers.Add(item);
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
                Customer customerFromDb = await ReadAsync(key,false,false);
                if (customerFromDb == null)
                {
                    dbContext.Customers.Remove(customerFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("This customer does not excist!");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<ICollection<Customer>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Customer> query= dbContext.Customers;
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

        public async Task<Customer> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Customer> query = dbContext.Customers;
                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Orders);
                }
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.FirstOrDefaultAsync(c=>c.Id == key);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateAsync(Customer item, bool useNavigationalProperties = false)
        {
            try
            {
                Customer customerFromDb = await ReadAsync(item.Id, useNavigationalProperties);
                if (customerFromDb==null)
                {
                    await CreateAsync(item);
                    return;
                }
                customerFromDb.Name = item.Name;
                customerFromDb.Password = item.Password;
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
                    customerFromDb.Orders = orders;
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
