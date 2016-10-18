using System.Threading.Tasks;
using Orleans;
using OrderManagement.Interfaces;
using System;
using Orleans.Providers;

namespace OrderManagement.Statefuls
{
    [StorageProvider(ProviderName = "PubSubStore")]
    public class Product : Grain<ProductInfo>, IProduct
    {
        public Task<ProductInfo> Get()
        {
            return Task.FromResult(State);
        }

        public async Task Create(string title, string category)
        {
            long id = this.GetPrimaryKeyLong();
            State.Id = id;
            State.Title = title;
            State.Category = category;
            await WriteStateAsync();
        }

        public async Task Price(decimal price)
        {
            State.Price = price;
            await WriteStateAsync();
        }

        public override Task OnActivateAsync()
        {

            return base.OnActivateAsync();
        }
    }
}
