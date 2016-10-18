using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Interfaces
{
    public interface IStockReporter : IGrainObserver
    {
        void StockUpdated(string data);
    }
}
