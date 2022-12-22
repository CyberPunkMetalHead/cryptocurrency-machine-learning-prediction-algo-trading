using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using CryptoPricePrediction.Utils;
using CryptoPricePrediction.Models;

namespace CryptoPricePrediction.Services
{
    public class PriceService
    {
        static readonly HttpClient client = new HttpClient();
        Util utils = new();

        public async Task<KlineResponse> GetPrices()
        {
            var query = new Dictionary<string, string>()
            {
                ["symbol"] = "BTCUSDT",
                ["interval"] = "1h",
                ["limit"] = "1",
            };

            var uri = QueryHelpers.AddQueryString("https://api.binance.com/api/v3/klines", query);
            var result = await client.GetAsync(uri);
            var content = result.Content.ReadAsStringAsync();
            List<List<object>> kline = JsonSerializer.Deserialize<List<List<object>>>(content.Result);


            KlineResponse response = new();
            response.OpenPrice = kline[0][1].ToString();
            response.ClosePrice = kline[0][4].ToString();
            response.OpenTime = utils.UnixTimeToDateTime(Convert.ToInt64(kline[0][0].ToString()));

            return response;
        }

    }
}
