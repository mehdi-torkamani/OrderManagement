using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Interfaces
{
    public class ProductInfo
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Title}: ${Price}";
        }
    }
}
