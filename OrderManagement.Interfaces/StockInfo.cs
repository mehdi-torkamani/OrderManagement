using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Interfaces
{
    public class StockInfo
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceChange { get; set; }
        public string PercentageChange { get; set; }

    }

}
