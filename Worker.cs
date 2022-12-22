using CryptoPricePrediction.Models;
using CryptoPricePrediction.Services;

namespace CryptoPricePrediction
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TradeService tradeService = new();
            PriceService priceService = new();

            while (!stoppingToken.IsCancellationRequested)
            {
                KlineResponse currentKline = await priceService.GetPrices();

                if(currentKline == null) continue;
                var closePrice = Convert.ToDouble(currentKline.ClosePrice);
                var openPrice = Convert.ToDouble(currentKline.OpenPrice);

                //Load sample data
                var klineOpenValue = new PricePrediction.ModelInput()
                {
                    Open = (float)openPrice
                };

                //Load model and predict output
                var prediction  = PricePrediction.Predict(klineOpenValue).Score;

                if (prediction < openPrice)
                {
                    Console.WriteLine("Prediction is smaller than the open price, skipping buy and awaiting new prediction");
                    Thread.Sleep(1000 * 3600);
                    continue;
                }

                //await tradeService.BuyBTC(50);
                await tradeService.TestBuyBTC(50);

                DateTime now = DateTime.Now;
                Console.WriteLine($"{DateTime.Now} Buying BTCUSDT. Current price is {closePrice}, predicted High is {prediction}. Working...");

                while(now < now.AddHours(1))
                {
                    KlineResponse latestPriceChecker = await priceService.GetPrices();
                    var latestPrice = Convert.ToDouble(latestPriceChecker.ClosePrice);

                    if (latestPrice >= prediction)
                    {
                        Console.WriteLine($"{DateTime.Now} Prediction reached, closing trade, taking profit. Predicted value was {prediction}, price reached is {latestPrice}.");
                        
                        //await tradeService.SellBTC(50);
                        await tradeService.TestSellBTC(50);

                    }

                    Thread.Sleep(1000);
                    //Console.WriteLine($"{DateTime.Now} current price is {latestPrice}");
                }

                Console.WriteLine($"{DateTime.Now} model efficiency lost, selling previously bought BTC");
                
                //await tradeService.SellBTC(50);
                await tradeService.TestSellBTC(50);


            }
        }
    }
}