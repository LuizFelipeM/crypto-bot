using CryptoBot.Application.Binance.Client.Streams;
using CryptoBot.Application.Binance.Contract;
using CryptoBot.Application.Binance.Contract.Interfaces;
using CryptoBot.CrossCutting.DTOs;
using CryptoBot.CrossCutting.Enums;
using CryptoBot.CrossCutting.Utils;
using CryptoBot.Domain.Interfaces.Services;
using CryptoBot.Infrastructure.Service.Contracts;
using CryptoBot.Infrastructure.Service.Ingestor;
using CryptoBot.Infrastructure.Service.Observers;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using Interval = CryptoBot.Domain.Models.Types.Interval;

namespace CryptoBot.Infrastructure.Service.Historical;

public class HistoricalService : IHistoricalService
{
    private readonly ILogger<HistoricalService> _logger;
    private readonly IBinanceHistoricalClient _binanceHistoricalClient;
    private readonly IIngestorProducer<IEnumerable<KlineContract>> _ingestor;
    private readonly IKlineObservable _klineObservable;

    public HistoricalService(
        ILogger<HistoricalService> logger,
        IBinanceHistoricalClient binanceHistoricalClient,
        IIngestorProducer<IEnumerable<KlineContract>> ingestor,
        IKlineObservable klineObservable
    )
    {
        _logger = logger;
        _binanceHistoricalClient = binanceHistoricalClient;
        _ingestor = ingestor;
        _klineObservable = klineObservable;
    }

    public void SubscribeBtc() => _klineObservable.Subscribe(new OneInchBtcObserver(),
                                                             Binance.Spot.Models.Interval.ONE_MINUTE);

    public void SubscribeUsdt() => _klineObservable.Subscribe(new OneInchUsdtObserver(),
                                                              Binance.Spot.Models.Interval.ONE_MINUTE);

    public async Task<IEnumerable<KlineDto>> GetKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime) =>
        await _binanceHistoricalClient.GetKlines(symbol, TinyMapper.Map<Binance.Spot.Models.Interval>(interval), startTime, endTime);

    public async Task Publish(IEnumerable<KlineDto> kline) =>
        await _ingestor.Publish(TinyMapper.Map<IEnumerable<KlineContract>>(kline));

    public async Task IngestKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime)
    {
        // var i = TinyMapper.Map<Binance.Spot.Models.Interval>(interval);
        var klines = await _binanceHistoricalClient.GetKlines(symbol, Binance.Spot.Models.Interval.ONE_MINUTE, startTime, endTime);
        var messages = klines.Split(250, Unit.Megabytes);
    }
}