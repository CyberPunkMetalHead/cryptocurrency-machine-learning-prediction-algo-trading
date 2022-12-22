using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;

namespace CryptoPricePrediction.Services
{
    public class TradeService
    {

        public BinanceClient binanceClient = new BinanceClient(new BinanceClientOptions
        {
            ApiCredentials = new ApiCredentials("YOUR_API_KEY","YOUR_API_SECRET"),
            SpotApiOptions = new BinanceApiClientOptions
            {
                BaseAddress = "https://api.binance.com/",
                RateLimitingBehaviour = RateLimitingBehaviour.Fail
            },
        });

        public async Task BuyBTC(int quantity)
        {
            var orderData = await binanceClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Market,
                quoteQuantity: quantity);

        }


        public async Task SellBTC(int quantity)
        {
            var orderData = await binanceClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Sell,
                SpotOrderType.Market,
                quoteQuantity: quantity);
        }

        public async Task TestBuyBTC(int quantity)
        {
            var result = await binanceClient.SpotApi.Trading.PlaceTestOrderAsync("BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Market,
                quoteQuantity: quantity);
        }

        public async Task TestSellBTC(int quantity)
        {
            var result = await binanceClient.SpotApi.Trading.PlaceTestOrderAsync("BTCUSDT",
                OrderSide.Sell,
                SpotOrderType.Market,
                quoteQuantity: quantity);
        }

    }
}
