using Business_Layer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CustomerManager : IManager
    {
        private readonly CustomerContext customerContext;

        public CustomerManager(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }

        public async Task CreateAsync(Customer customer)
        {
            await customerContext.CreateAsync(customer);
        }

        public async Task<Customer> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            return await customerContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<Customer>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await customerContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Customer customer, bool useNavigationalProperties = false)
        {
            await customerContext.UpdateAsync(customer, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await customerContext.DeleteAsync(key);
        }

    }
}
