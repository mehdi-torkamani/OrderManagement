using OrderManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Client
{
    class StockReporter : IStockReporter
    {
        //private IStock Stock { get; set; }

        //public StockReporter(IStock stock)
        //{
        //    //Stock = stock;
        //    //stock.Subscribe()
        //}

        //public void StockUpdated(StockChange data)
        //{
        //    Console.WriteLine(data);
        //}

        public void StockUpdated(string data)
        {
            Console.WriteLine(data);
        }
    }
}
