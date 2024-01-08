using Business_Layer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CouponManager
    {
        private readonly CouponContext couponContext;

        public CouponManager(CouponContext couponContext)
        {
            this.couponContext = couponContext;
        }

        public async Task CreateAsync(Coupon coupon)
        {
            await couponContext.CreateAsync(coupon);
        }

        public async Task<Coupon> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await couponContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<Coupon>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await couponContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Coupon coupon, bool useNavigationalProperties = false)
        {
            await couponContext.UpdateAsync(coupon, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await couponContext.DeleteAsync(key);
        }

    }
}
