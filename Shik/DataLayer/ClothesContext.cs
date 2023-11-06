using Business_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ClothesContext : IDB<Clothes, int>
    {
        private readonly ShikDBContext dbContext;
        public ClothesContext(ShikDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        public async Task CreateAsync(Clothes item)
        {
            try
            {
                dbContext.Clothes.Add(item);
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
                Clothes clothesFromDb = await ReadAsync(key);
                if (clothesFromDb != null)
                {
                    dbContext.Clothes.Remove(clothesFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("This kind of clothes does not excist");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<ICollection<Clothes>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Clothes> query = dbContext.Clothes;
                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.Orders).Include(c=>c.Customers);
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

        public async Task<Clothes> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Clothes> query = dbContext.Clothes;
                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.Orders).Include(c=>c.Customers);
                }
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.FirstOrDefaultAsync(c=>c.Id==key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Clothes item, bool useNavigationalProperties = false)
        {
            try
            {
                Clothes clothesFromDb =await ReadAsync(item.Id, useNavigationalProperties,false);
                if (clothesFromDb == null)
                {
                    await CreateAsync(item);
                    return;
                }

                clothesFromDb.Name = item.Name;
                clothesFromDb.Price= item.Price;
                clothesFromDb.Description= item.Description;
                clothesFromDb.Size= item.Size;
                if (useNavigationalProperties)
                {
                    List<OrderClothes> orderClothes = new List<OrderClothes>();

                    foreach (OrderClothes oc in item.Orders)
                    {
                        OrderClothes poFromDb = dbContext.OrdersClothes.Find(oc.ClothesId, oc.OrderId);

                        if (poFromDb != null)
                        {
                            orderClothes.Add(poFromDb);
                        }
                        else
                        {
                            orderClothes.Add(oc);
                        }
                    }
                    clothesFromDb.Orders = orderClothes;
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
