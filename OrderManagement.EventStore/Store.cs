using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using System.Net;
using System.IO;
using Google.Protobuf;
using OrderManagement.EventStore.Messages;
using Google.Protobuf.Collections;
using ServiceStack;


namespace OrderManagement.EventStore
{
    public class Store
    {
        public async void Do()
        {
            //var endpoint = new IPEndPoint(IPAddress.Loopback, 1113);
            //var connection = EventStoreConnection.Create(endpoint);

            var connectionString = "ConnectTo=tcp://admin:changeit@localhost:1113; HeartBeatTimeout=500";
            var connection = EventStoreConnection.Create(connectionString);
            await connection.ConnectAsync();
            //var data = GetData();
            var data = GetJsonData();
            //var result = await connection.AppendToStreamAsync("StockMessages", ExpectedVersion.EmptyStream, data);
            var result = await connection.AppendToStreamAsync("StockMessages", ExpectedVersion.Any, data);
        }

        public EventData GetData()
        {
            var message = GetMessage("AAPL", "Sydney", "ASX500");
            var messageBytes = Serialize(message);

            var data = new EventData(
                Guid.NewGuid(),
                "StockChanged",
                false, 
                messageBytes,
                null);
            return data;
        }

        public EventData GetJsonData()
        {
            var message = GetMessage("AAPL", "Sydney", "ASX500");
            var messageJson = message.ToJson();
            var messageBytes = Encoding.UTF8.GetBytes(messageJson);

            var data = new EventData(
                Guid.NewGuid(),
                "StockChanged",
                true,
                messageBytes,
                null);
            return data;
        }

        public StockMessage GetMessage(string symbol, string city, string marketSymbol)
        {
            var message = new StockMessage
            {
                Symbol = symbol,
                Id = Guid.NewGuid().ToString(),
                Change = new StockChange
                {
                    Price = 114.55,
                    PriceChange = -1.08,
                    PercentageChange = 0.05,
                    DateTime = DateTime.UtcNow.ToString()
                },
                Market = new Market
                {
                    City = city,
                    Symbol = marketSymbol,
                    Status = MarketStatus.Closed
                }
            };

            message.Comments.Add(
                new Comment
                {
                    Id = 1,
                    Title = "Fixed Item",
                    Content = "Item has been fixed before",
                    DateTime = DateTime.UtcNow.ToString()
                });
            message.Comments.Add(
                new Comment
                {
                    Id = 2,
                    Title = "Fixed Item",
                    Content = "Item has been fixed again",
                    DateTime = DateTime.UtcNow.ToString()
                });

            return message;
        }

        public byte[] Serialize(StockMessage message)
        {
            byte[] messageBytes = null;

            using (var stream = new MemoryStream())
            {
                //using (var output = new CodedOutputStream(stream))
                //{
                message.WriteTo(stream);
                messageBytes = stream.ToArray();
                //}
            }

            return messageBytes;
        }

        public StockMessage Deserialize(byte[] messageBytes)
        {
            StockMessage message = null;

            //using (var stream = new MemoryStream(messageBytes))
            //{
            //using (var output = new CodedOutputStream(stream))
            //{
            message = StockMessage.Parser.ParseFrom(messageBytes);
                // message.WriteTo(stream);
                //}
            //}

            return message;
        }
    }
}
