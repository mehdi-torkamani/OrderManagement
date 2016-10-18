using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Interfaces
{
    public interface IProduct : IGrainWithIntegerKey
    {
        Task<ProductInfo> Get();

        Task Create(string title, string category);

        Task Price(decimal price);
    }
}
