using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;
using OrderManagement.Interfaces;
using System;

namespace OrderManagement.Implementation
{
    [StorageProvider(ProviderName = "memory")]
    public class Stock : Grain<StockChange>, IStock
    {
        private string Symbol { get; set; }
        private ObserverSubscriptionManager<IStockReporter> Observers { get; set; }
            = new ObserverSubscriptionManager<IStockReporter>();

        public async Task Update(decimal change)
        {
            var newStock = new StockChange
            {
                Symbol = Symbol,
                Price = State.Price + change,
                Change = change,
                DateTime = DateTime.UtcNow
            };
            State = newStock;
            Observers.Notify(observer => observer.StockUpdated(newStock.ToString()));
            await WriteStateAsync();
            //return TaskDone.Done;
        }

        public Task Subscribe(IStockReporter observer)
        {
            if (!Observers.IsSubscribed(observer))
                Observers.Subscribe(observer);
            return TaskDone.Done;
        }

        public Task Unsubscribe(IStockReporter observer)
        {
            if (Observers.IsSubscribed(observer))
                Observers.Unsubscribe(observer);
            return TaskDone.Done;
        }

        public async Task Refresh(object _)
        {
            var priceGrain = GrainFactory.GetGrain<IStockPrice>(Symbol);
            var change = await priceGrain.GetPrice();
            await Update(change);
        }

        public override Task OnActivateAsync()
        {
            Symbol = this.GetPrimaryKeyString();
            RegisterTimer(Refresh, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
            return base.OnActivateAsync();
        }
    }
}
