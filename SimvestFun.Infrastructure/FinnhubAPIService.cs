using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.Infrastructure
{
    public class FinnhubAPIService: IFinnhubAPIService
    {
        private readonly string _host;
        private readonly string _key;

        public FinnhubAPIService(IConfiguration _config)
        {
            _host = _config.GetSection("StocksAPI").GetSection("Host").Value;
            _key = _config.GetSection("StocksAPI").GetSection("Key").Value;
        }

        public async Task<decimal> GetStockPriceAsync(string symbol)
        {
            var client = new HttpClient();
            HttpRequestMessage stocksAPIRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}"),
                Headers =
                {
                    { _host, _key },
                },
            };

            using (var response = await client.SendAsync(stocksAPIRequest))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var price = (decimal)json["c"];
                return decimal.Round(price, 2);
            }
        }
    }
}
