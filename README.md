# Machine Learning Trading Bot for Bitcoin

This is a functional trading algorithm that aims to predict the price of Bitcoin within the next hour, and places trades based on its prediction. The model was trained on 1 hour candlestick data for the Open and High price and its job is to take the new Open price for Bitcoin, and predict the likely high price for the next hour.

For a detailed explanation of how the machine learning trading bot works in detail and how it was built see [this article](https://cryptomaton.medium.com/i-built-a-trading-algorithm-that-predicts-the-price-of-bitcoin-854258295a3f)



#### How to use the ML Bitcoin Trading Bot
1. Clone this repository
2. Install Visual studio 2022 or similar
3. Run dotnet restore
4. Configure your bot in the config.json.example file and remove .example once finished adding your api key, secret, buy amount and whether the bot should start in test more or not.
5. Build the application (Ctrl+Shift+B)
6. Run the app from within Visual Studio or run the executable in "projectDirectory/bin/Debug/net6.0/CryptoPricePrediction.exe"

####  Considerations
By default, the bot will place test orders. Use the config.json.example file in order to switch to live trading or to modify the amount bought per trade.
Daily logs are dropped in the "Logs" directory. 

#### Disclaimer
Use this at your own risk and peril. The tool works (functionally) but the efficiency of this strategy hasn't been tested.

#### Missing features
This is just a quick proof of concept and could be fleshed out. I may add some more functionality in the future, but if you want to contribute by making a pull request here are some features that you can add to the tool:

 - Creating models for the Binance Kline response, to avoid converting / casting.