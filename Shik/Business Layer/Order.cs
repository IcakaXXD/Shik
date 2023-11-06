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

        [Required]
        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public List<Clothes> Clothes { get; set; }

        public Order()
        {
            Clothes = new List<Clothes>();
        }
        public Order(string address, Customer customer)
        {
            Address = address;
            Date = DateTime.Now;
            Status = OrderStatus.New;
            Customer = customer;
            Clothes = new List<Clothes>();
        }
    }
}
