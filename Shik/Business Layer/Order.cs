using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Order
    {
        [Key]
        public int Id { get; private set; }
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(50, ErrorMessage = "Address cannot be more than 50 symbols!")]
        public string Address { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(20)]
        public OrderStatus Status { get; set; }

        
        public Customer Customer { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Shipping Shipping { get; set; }

        [ForeignKey("Shipping")]
        public int ShippingId { get; set; }
        
        public List<OrderClothes> OrderClothes { get; set; }
        public List<Coupon> Coupons { get; set; }
        public Order()
        {
            OrderClothes = new List<OrderClothes>();
            
            Coupons = new List<Coupon>();
        }
        public Order(string address, Customer customer,Shipping shipping)
        {
            Shipping= shipping;
            Address = address;
            Date = DateTime.Now;
            Status = OrderStatus.New;
            Customer = customer;
            Coupons=new List<Coupon>();
            OrderClothes= new List<OrderClothes>();
            
        }
    }
}
