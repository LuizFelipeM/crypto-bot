using CryptoBot.CrossCutting.DTOs;

namespace CryptoBot.Infrastructure.Service.Interfaces.Historical;

public interface IHistoricalService
{
    Task Publish(KlineDto kline);
}