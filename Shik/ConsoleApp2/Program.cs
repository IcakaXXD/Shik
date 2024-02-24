using Business_Layer;
using ServiceLayer;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CustomerManager customerManager;
            OrderManager orderManager;
            ClothesManager clothesManager;
            CouponManager couponManager;
            ShippingManager shippingManager;

            customerManager = new CustomerManager(ContextHelper.GetCustomerContext());
            orderManager = new OrderManager(ContextHelper.GetOrderContext());
            clothesManager = new ClothesManager(ContextHelper.GetClothesContext());
            couponManager = new CouponManager(ContextHelper.GetCouponContext());
            shippingManager = new ShippingManager(ContextHelper.GetShippingContext());


            Customer c = await customerManager.ReadAsync(int.Parse(Console.ReadLine()));


            
            Console.ReadKey();
        }
    }
}
