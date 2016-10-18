using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using OrderManagement.Interfaces;

namespace OrderManagement.EventStore.Hub
{
    public class Hub
    {
        public const string Publisher = "OrderManager";

        public string ManagerConnectionString { get; set; } = "Endpoint=sb://lorien.servicebus.windows.net/;SharedAccessKeyName=Manager;SharedAccessKey=WI8K2CnvtmrQQidysWAq11zfqj99a4FMsm52NK+vnt0=;EntityPath=stockchanges";
        public string PublisherConnectionString { get; set; } = "Endpoint=sb://lorien.servicebus.windows.net/;SharedAccessKeyName=Publisher;SharedAccessKey=BLUIRHZEC4V63wkPCVxaroiPqTtQLB6UfJtj0I4fyNU=;EntityPath=stockchanges";
        public string SubscriberConnectionString { get; set; } = "Endpoint=sb://lorien.servicebus.windows.net/;SharedAccessKeyName=Subscriber;SharedAccessKey=16xdfUT2thl5x6RKpsMI+nTMuFVKXpd88HSU1Ws4KQI=;EntityPath=stockchanges";

        public EventData GetData(StockChange change)
        {
            var json = change.ToJson();

            var content = Encoding.UTF8.GetBytes(json);
            var data = new EventData(content);
            return data;
        }

        public object GetChangeData()
        {
            var change = new StockChange
            {

                Symbol = "AAPL",
                Price = 110.55m,
                Change = 1.09m,
                DateTime = DateTime.UtcNow
            };

            return change;
        }
    }
}
