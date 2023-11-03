using CryptoBot.CrossCutting.DTOs;
using CryptoBot.Domain.Models.Types;

namespace CryptoBot.Domain.Interfaces.Services;

public interface IHistoricalService
{
    void SubscribeBtc();
    void SubscribeUsdt();
    Task Publish(IEnumerable<KlineDto> kline);
    Task IngestKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime);
}