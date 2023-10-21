namespace CryptoBot.Application.Binance.Contract.Interfaces;

public interface IBinanceMarketClient
{
    Task Subscribe(string stream, Func<string, Task> onMessageReceived);
}