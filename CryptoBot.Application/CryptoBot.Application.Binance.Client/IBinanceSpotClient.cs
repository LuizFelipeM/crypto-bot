using CryptoBot.Application.Binance.Client.API;

namespace CryptoBot.Application.Binance.Client;

public interface IBinanceSpotClient
{
    Task<string> NewOrder(NewOrderDto newOrder);
}