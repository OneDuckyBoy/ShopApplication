using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Orderdate { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
