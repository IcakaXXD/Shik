using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ContextHelper
    {
        private static ShikDBContext dbContext;
        private static CustomerContext customerContext;
        private static ClothesContext clothesContext;
        private static CouponContext couponContext;
        private static OrderContext orderContext;
        private static ShippingContext shippingContext;

        public static ShikDBContext GetDbContext()
        {
            if (dbContext == null)
            {
                SetDbContext();
            }

            return dbContext;
        }

        public static void SetDbContext()
        {
            dbContext = new ShikDBContext();
        }

        public static CustomerContext GetCustomerContext()
        {
            if (customerContext == null)
            {
                SetCustomerContext();
            }

            return customerContext;
        }

        public static void SetCustomerContext()
        {
            customerContext = new CustomerContext(GetDbContext());
        }

        public static ClothesContext GetClothesContext()
        {
            if (clothesContext == null)
            {
                SetClothesContext();
            }

            return clothesContext;
        }

        public static void SetClothesContext()
        {
            clothesContext = new ClothesContext(GetDbContext());
        }


        public static CouponContext GetCouponContext()
        {
            if (couponContext == null)
            {
                SetCouponContext();
            }

            return couponContext;
        }

        public static void SetCouponContext()
        {
            couponContext = new CouponContext(GetDbContext());
        }

        public static OrderContext GetOrderContext()
        {
            if (orderContext == null)
            {
                SetOrderContext();
            }

            return orderContext;
        }

        public static void SetOrderContext()
        {
            orderContext = new OrderContext(GetDbContext());
        }


        public static ShippingContext GetShippingContext()
        {
            if (shippingContext == null)
            {
                SetShippingContext();
            }

            return shippingContext;
        }

        public static void SetShippingContext()
        {
            shippingContext = new ShippingContext(GetDbContext());
        }
    }
}
