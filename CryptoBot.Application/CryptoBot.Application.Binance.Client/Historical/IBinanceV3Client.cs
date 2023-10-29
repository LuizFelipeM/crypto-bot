using Refit;

namespace CryptoBot.Application.Binance.Client.Historical;

public interface IBinanceV3Client
{
    [Get("/klines")]
    Task<IEnumerable<IEnumerable<dynamic>>> GetKlines([Query] string symbol, [Query] string interval, [Query] long startTime, [Query] long endTime, [Query] int limit = 1000);
}