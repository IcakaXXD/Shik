using Business_Layer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class OrderManager
    {
        private readonly OrderContext orderContext;

        public OrderManager(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task CreateAsync(Order order)
        {
            await orderContext.CreateAsync(order);
        }

        public async Task<Order> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await orderContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<Order>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await orderContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Order order, bool useNavigationalProperties = false)
        {
            await orderContext.UpdateAsync(order, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await orderContext.DeleteAsync(key);
        }

    }
}
