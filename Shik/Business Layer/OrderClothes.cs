using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class OrderClothes
    {
        [ForeignKey("Clothes")]
        public int ClothesId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public Clothes Clothes { get; set; }

        public Order Order { get; set; }
    }
}
