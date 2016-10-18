using OrderManagement.EventStore.Hub;
using OrderManagement.EventStore.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.EventStore.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            var message = store.GetMessage("AAPL", "Sydney", "ASX500");
            var data = store.Serialize(message);
            var newMessage = store.Deserialize(data);
            Console.WriteLine($"{newMessage.Symbol} in {newMessage.Market.Symbol}({newMessage.Market.City}:{newMessage.Market.Status})");

            store.Do();

            StockChangePublisher publisher = new StockChangePublisher();
            var changeData = new Hub.Hub().GetChangeData();
            publisher.Send(changeData);

            Console.ReadLine();
        }
    }
}
