using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be more than 50 symbols!")]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders= new List<Order>();
        }
        public Customer( string name)
        {
            Name = name;
            Orders= new List<Order>();
        }
    }
}
