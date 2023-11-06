using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business_Layer
{
    public class Clothes
    {
        public int Id { get; set; }
        public Size Size { get; set; }//?
        public string Color { get; set; }
        [MaxLength(100,ErrorMessage ="Too long name")]
        public string Name { get; set; }
        [MaxLength(3000, ErrorMessage = "Too long name")]
        public string Description { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="This number does not exist")]
        public decimal Price { get; set; }
        public List<Customer> Customers { get; set; }
        public List<OrderClothes> Orders { get; set; }
        public Clothes()
        {
            Customers = new List<Customer>();
            Orders = new List<OrderClothes>();
        }
        public Clothes( string color, string name, string decription, decimal price)
        {
            Customers = new List<Customer>();
            Orders = new List<OrderClothes>();
            Size =Size.New;
            Color = color;
            Name = name;
            Description = decription;
            Price = price;
        }
    }
}
