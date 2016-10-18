using OrderManagement.Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Text;

namespace OrderManagement.Implementation
{
    public class StockPrice : Grain, IStockPrice
    {
        private string Symbol => this.GetPrimaryKeyString();

        public async Task<decimal> GetPrice()
        {
            var stockUri = $"http://download.finance.yahoo.com/d/quotes.csv?f=snl1c1p2&e=.csv&s={Symbol}";
            var client = new WebClient();
            var value = await client.DownloadStringTaskAsync(stockUri);
            var pieces = value.Split(',');
            var info = new StockInfo
            {
                Symbol = pieces[0].Trim('"'),
                Name = pieces[1].Trim('"'),
                Price = decimal.Parse(pieces[2]),
                PriceChange = decimal.Parse(pieces[3]),
                PercentageChange = pieces[4].Trim('"')
            };
            //var info = value.FromCsv<StockInfo>();
            return info.Price;
        }
    }
}
