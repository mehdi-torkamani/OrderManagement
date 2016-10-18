using System;
using OrderManagement.Interfaces;
using Orleans;
using Orleans.Runtime.Configuration;
using static System.Console;

namespace OrderManagement.Client
{
    class Program
    {
        static Guid AppId;

        static void Main(string[] args)
        {
            WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
            ReadLine();

            var config = ClientConfiguration.LocalhostSilo(30000);
            config.DefaultTraceLevel = Orleans.Runtime.Severity.Error;
            GrainClient.Initialize(config);

            WriteLine("----------------------------------------------------");

            //PubSub();
            ProductRepo();

            ReadLine();
        }

        static void ProductRepo()
        {
            var item1 = GrainClient.GrainFactory.GetGrain<IProduct>(1);
            //item1.Create("Item 1", "Books");
            //item1.Price(100);
            //item1.Price(120);
            //item1.Price(300);

            WriteLine("Wanna see the product?");
            ReadLine();

            var product = item1.Get().Result;
            WriteLine(product);
        }

        static void PubSub()
        {
            var aapl = GrainClient.GrainFactory.GetGrain<IStock>("AAPL");
            //var msft = GrainClient.GrainFactory.GetGrain<IStock>("MSFT");
            //var goog = GrainClient.GrainFactory.GetGrain<IStock>("GOOG");

            //var aaplObserver1 = GrainClient.GrainFactory.GetGrain<IStockObserver>(1, "AAPL", null);
            //var aaplObserver2 = GrainClient.GrainFactory.GetGrain<IStockObserver>(2, "AAPL", null);
            //var googObserver1 = GrainClient.GrainFactory.GetGrain<IStockObserver>(1, "GOOG", null);
            //var msftObserver1 = GrainClient.GrainFactory.GetGrain<IStockObserver>(1, "MSFT", null);

            var aaplObserver_1 = new StockReporter();
            var aaplObserver_1Reference = 
                GrainClient.GrainFactory.CreateObjectReference<IStockReporter>(aaplObserver_1).Result;
            var aaplObserver_2 = new StockReporter();
            var aaplObserver_2Reference =
                GrainClient.GrainFactory.CreateObjectReference<IStockReporter>(aaplObserver_2).Result;
            //var googObserver_1 = new StockReporter(goog);
            //var msftObserver_1 = new StockReporter(msft);

            aapl.Subscribe(aaplObserver_1Reference).Wait();
            aapl.Subscribe(aaplObserver_2Reference).Wait();

            //aaplObserver1.Register().Wait();
            //aaplObserver2.Register().Wait();
            //googObserver1.Register().Wait();
            //msftObserver1.Register().Wait();

            ////ReadLine();

            aapl.Update(100);
            aapl.Update(4);
            aapl.Update(-3);
            //aapl.Update(-6);
            //aapl.Update(5);
        }
    }
}
