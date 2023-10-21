using CryptoBot.Application.Binance.Contract.DTOs;

namespace CryptoBot.Application.Binance.Contract.Interfaces;

public interface IBinanceSpotClient
{
    Task<string> NewOrder(NewOrderDto newOrder);
}