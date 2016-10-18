using Microsoft.ServiceBus.Messaging;
using OrderManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.EventStore.Hub
{
    public class StockChangePublisher
    {
        private void Send(StockChange change)
        {
            var hub = new Hub();
            var client = EventHubClient.CreateFromConnectionString(hub.PublisherConnectionString);
            var sender = client.CreateSenderAsync(Hub.Publisher).Result;
            var data = hub.GetData(change);
            sender.SendAsync(data).Wait();
        }

        public void Send(object change)
        {
            Send(change as StockChange);
            Send(change as StockChange);
            Send(change as StockChange);
            Send(change as StockChange);
            Send(change as StockChange);
            Send(change as StockChange);
            Send(change as StockChange);
            Send(change as StockChange);
        }
    }
}
