using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Shipping
    {
        [Key]
        public int Id { get; set; }
        public Shipping_method Shipping_Method { get; set; }
        public DateTime DeliveryTime { get; set; }
        [Range(0, int.MaxValue,ErrorMessage ="Invalid cost")]
        public decimal Shipping_cost{ get; set; }
        public List<Order> Orders { get; set; }
        public Shipping(decimal shipping_cost)
        {
            Shipping_cost = shipping_cost;
            Shipping_Method = Shipping_method.New;
            Orders = new List<Order>();
        }
        public Shipping()
        {
            Orders= new List<Order>();
        }
    }
}
