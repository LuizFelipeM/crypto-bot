namespace CryptoBot.Application.Binance.Client;

public interface IBinanceMarketClient
{
    Task Subscribe(string stream, Func<string, Task> onMessageReceived);
}