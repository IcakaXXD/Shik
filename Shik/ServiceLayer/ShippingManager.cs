using Business_Layer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ShippingManager : IManager
    {
        private readonly ShippingContext shippingContext;

        public ShippingManager(ShippingContext shippingContext)
        {
            this.shippingContext = shippingContext;
        }

        public async Task CreateAsync(Shipping shipping)
        {
            await shippingContext.CreateAsync(shipping);
        }

        public async Task<Shipping> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await shippingContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<Shipping>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await shippingContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Shipping shipping, bool useNavigationalProperties = false)
        {
            await shippingContext.UpdateAsync(shipping, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await shippingContext.DeleteAsync(key);
        }

    }
}
