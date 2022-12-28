using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoPricePrediction.Models;
using CryptoPricePrediction.Utils;


namespace CryptoPricePrediction.Services
{
    public class TradeService
    {
        Util utils = new();
        public string? apiKey;
        public string? secret;
        public int amount;
        public bool testMode;
        BinanceClient client;

        public TradeService() {

            Root config = utils.LoadJson()!;
            apiKey = config.Authentication.Key;
            secret = config.Authentication.Secret;
            amount = config.Settings.BuyAmount;
            testMode = config.Settings.TestMode;
            client = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new ApiCredentials(apiKey, secret),
                SpotApiOptions = new BinanceApiClientOptions
                {
                    BaseAddress = "https://api.binance.com/",
                    RateLimitingBehaviour = RateLimitingBehaviour.Fail
                },
            });
        }

        public async Task BuyBTC()
        {
            if (testMode)
            {
                await client.SpotApi.Trading.PlaceTestOrderAsync("BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Market,
                quoteQuantity: amount);
            }

            else
            {
                await client.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Market,
                quoteQuantity: amount);
            }
        }

        public async Task SellBTC()
        {

            if (testMode)
            {
               await client.SpotApi.Trading.PlaceTestOrderAsync("BTCUSDT",
                OrderSide.Sell,
                SpotOrderType.Market,
                quoteQuantity: amount);
            }

            else
            {
                await client.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Sell,
                SpotOrderType.Market,
                quoteQuantity: amount);
            }
        }
    }
}
