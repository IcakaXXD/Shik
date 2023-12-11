using Business_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CouponContext : IDB<Coupon, int>
    {
        private readonly ShikDBContext dbContext;
        public CouponContext(ShikDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        public async Task CreateAsync(Coupon item)
        {
            try
            {
                Order orderFromDb = await dbContext.Orders.FindAsync(item.OrderId);

                if (orderFromDb != null)
                {
                    item.Order = orderFromDb;
                }
                dbContext.Coupons.Add(item);
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
                Coupon couponFromDb = await ReadAsync(key, false, false);

                if (couponFromDb != null)
                {
                    dbContext.Coupons.Remove(couponFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Coupon with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Coupon>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Coupon> query = dbContext.Coupons;

                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Order);
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

        public async Task<Coupon> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Coupon> query = dbContext.Coupons;

                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Order);
                       
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

        public async Task UpdateAsync(Coupon item, bool useNavigationalProperties = false)
        {
            try
            {
                Coupon couponFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);
                couponFromDb.Discount_amount = item.Discount_amount;
                couponFromDb.Exparation_date = item.Exparation_date;
                if (useNavigationalProperties)
                {
                    Order orderFromDb = await dbContext.Orders.FindAsync(item.OrderId);

                    if (orderFromDb != null)
                    {
                        couponFromDb.Order = orderFromDb;
                    }
                    else
                    {
                        couponFromDb.Order = item.Order;
                    }
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
