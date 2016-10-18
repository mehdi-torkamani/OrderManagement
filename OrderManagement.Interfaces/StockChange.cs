using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Interfaces
{
    public class StockChange
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
        public DateTime DateTime { get; set; }

        private string ChangeValue
        {
            get
            {
                if (Change == 0) return Change.ToString();
                return (Change > 0) ? $"+ {Change}" : $"- {Change}";
            }
        }

        public override string ToString()
        {
            return $"{Symbol} changed to ${Price} ({ChangeValue}) at {DateTime}";
        }
    }
}
