# Machine Learning Trading Bot for Bitcoin

This is a functional trading algorithm that aims to predict the price of Bitcoin within the next hour, and places trades based on its prediction. The model was trained on 1 hour candlestick data for the Open and High price and its job is to take the new Open price for Bitcoin, and predict the likely high price for the next hour.

Detailed explanation of how the machine learning trading bot works in detail.


#### How to use the ML Bitcoin Trading Bot
1. Clone this repository
2. Install Visual studio 2022 or similar
3. Run dotnet restore
4. Add your Binance API_KEY and API_SECRET inside the Binance client in TradeService.cs
5. Run Program.cs

####  Considerations
By default, the bot will place test orders. To enable live trading, replace all instances of:
                          
    await tradeService.TestSellBTC(50);

with:

      //await tradeService.SellBTC(50);
The bot will now aim to Buy or Sell 50 USDT worth of bitcoin for each cycle.

#### Disclaimer
Use this at your own risk and peril. The tool works (functionally) but the efficiency of this strategy hasn't been tested.

#### Missing features
This is just a quick proof of concept and could be fleshed out. I may add some more functionality in the future, but if you want to contribute by making a pull request here are some features that you can add to the tool:

 - Creates and updates a log file. All the logs are currently dumped in the console.
 - Creating models for the Binance Kline response, to avoid converting / casting.
 - Create a config file where the API keys and trade amount can live.