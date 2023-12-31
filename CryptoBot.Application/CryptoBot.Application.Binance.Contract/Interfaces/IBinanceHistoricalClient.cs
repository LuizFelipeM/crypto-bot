using Binance.Spot.Models;
using CryptoBot.CrossCutting.DTOs;

namespace CryptoBot.Application.Binance.Contract.Interfaces;

public interface IBinanceHistoricalClient
{
    Task<IEnumerable<KlineDto>> GetKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime);
    IAsyncEnumerable<IEnumerable<KlineDto>> GetKlinesYield(string symbol, Interval interval, DateTime startTime, DateTime endTime);
}