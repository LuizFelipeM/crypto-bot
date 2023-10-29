using CryptoBot.CrossCutting.DTOs;

namespace CryptoBot.Application.Service.Interfaces.Historical;

public interface IHistoricalService
{
    Task Publish(KlineDto kline);
}