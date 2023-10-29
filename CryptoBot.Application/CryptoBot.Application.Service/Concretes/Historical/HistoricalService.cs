using Binance.Spot.Models;
using CryptoBot.Application.Binance.Contract.Interfaces;
using CryptoBot.Application.Service.Ingestor;
using CryptoBot.Application.Service.Interfaces.Historical;
using CryptoBot.CrossCutting.DTOs;
using CryptoBot.CrossCutting.Enums;
using CryptoBot.CrossCutting.Utils;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;

namespace CryptoBot.Application.Service.Historical;

public class HistoricalService : IHistoricalService
{
    private readonly ILogger<HistoricalService> _logger;
    private readonly IBinanceHistoricalClient _binanceHistoricalClient;
    private readonly IIngestorProducer<KlineContract> _ingestor;

    public HistoricalService(
        ILogger<HistoricalService> logger,
        IBinanceHistoricalClient binanceHistoricalClient,
        IIngestorProducer<KlineContract> ingestor
    )
    {
        _logger = logger;
        _binanceHistoricalClient = binanceHistoricalClient;
        _ingestor = ingestor;
    }

    public async Task<IEnumerable<KlineDto>> GetKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime) =>
        await _binanceHistoricalClient.GetKlines(symbol, interval, startTime, endTime);

    public async Task Publish(KlineDto kline) => await _ingestor.Publish(TinyMapper.Map<KlineContract>(kline));

    public async Task IngestKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime)
    {
        var klines = await _binanceHistoricalClient.GetKlines(symbol, interval, startTime, endTime);
        var klinesSize = klines.GetSize().ToSize(Unit.Megabytes);

        for (int i = 0; i < Convert.ToInt32(klinesSize / 256.0); i++)
        {

        }
    }
}