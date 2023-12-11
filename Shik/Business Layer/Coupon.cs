using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Range(0, 100,ErrorMessage ="Invalid percentage!")]
        public int Discount_amount { get; set; }//ще се въвежда в проценти
        public DateTime Exparation_date { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Coupon(int discount_amount,DateTime exparation_date,Order order)
        {
            discount_amount = Discount_amount;
            exparation_date = Exparation_date;
            Order = order;
        }
        public Coupon()
        {
             
        }
    }
}
