using Business_Layer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ClothesManager : IManager
    {
        private readonly ClothesContext clothesContext;

        public ClothesManager(ClothesContext clothesContext)
        {
            this.clothesContext = clothesContext;
        }

        public async Task CreateAsync(Clothes clothes)
        {
            await clothesContext.CreateAsync(clothes);
        }

        public async Task<Clothes> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await clothesContext.ReadAsync(key, useNavigationalProperties);
        }

        public async Task<ICollection<Clothes>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await clothesContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Clothes clothes, bool useNavigationalProperties = false)
        {
            await clothesContext.UpdateAsync(clothes, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await clothesContext.DeleteAsync(key);
        }

    }
}
