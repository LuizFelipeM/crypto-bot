using Binance.Spot.Models;
using CryptoBot.Application.Binance.Contract.DTOs;

namespace CryptoBot.Application.Binance.Contract.Interfaces;

public interface IBinanceHistoricalClient
{
    Task<IEnumerable<KlineDto>> GetKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime);
}