using CryptoPricePrediction.Models;
using CryptoPricePrediction.Services;
using System.Text;
using CryptoPricePrediction.Utils;
using Microsoft.Extensions.Options;

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
            Util utils = new();

            Root configOptions = utils.LoadJson();
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Logs/";

            if(configOptions.Settings.TestMode)
            {
                Console.WriteLine("Starting in Test Mode, trade logs will be dropped in the Logs Folder.");
            }
            else
            {
                Console.WriteLine("NOTE: You are about to start LIVE TRADING USING REAL FUNDS. App will Start Executing in 30 seconds.");
                Thread.Sleep(2000);

                for (int i = 30; i >= 0; i--)
                {
                    if(stoppingToken.IsCancellationRequested) { continue; }
                    Console.WriteLine($"You have {i} seconds to stop the application using Ctrl + C. ");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }

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
                StringBuilder sb = new StringBuilder();

                if (prediction < openPrice)
                {
                    File.AppendAllText(logPath + $"{DateTime.Now.Date.ToLongDateString()}.txt", $"{DateTime.Now} Prediction ({prediction}) is smaller than the open price ({openPrice}), skipping buy and awaiting new prediction \n");
                    Thread.Sleep(1000 * 3600);
                    continue;
                }

                await tradeService.BuyBTC();

               DateTime now = DateTime.Now;
               File.AppendAllText(logPath + $"{now.Date.ToLongDateString()}.txt", $"{now} Buying BTCUSDT. Current price is {closePrice}, predicted High is {prediction}. Working... \n");

                while (now < now.AddHours(1))
                {
                    KlineResponse latestPriceChecker = await priceService.GetPrices();
                    var latestPrice = Convert.ToDouble(latestPriceChecker.ClosePrice);

                    if (latestPrice >= prediction)
                    {
                        File.AppendAllText(logPath+$"{now.Date.ToLongDateString()}.txt", $"{DateTime.Now} Prediction reached, taking profit. Predicted value was {prediction}, price reached is {latestPrice}. PnL is {(latestPrice - openPrice)/openPrice*100} \n");
                        
                        await tradeService.SellBTC();
                        break;
                    }

                    Thread.Sleep(1000);
                    //Console.WriteLine($"{DateTime.Now} current price is {latestPrice}");
                }
                KlineResponse sellPriceChecker = await priceService.GetPrices();
                var sellPrice = Convert.ToDouble(sellPriceChecker.ClosePrice);
                File.AppendAllText(logPath + $"{now.Date.ToLongDateString()}.txt", $"{DateTime.Now} model efficiency lost, selling bought BTC. PnL is {(sellPrice - openPrice) / openPrice * 100} \n");
                
                await tradeService.SellBTC();

            }
        }
    }
}