using System.Diagnostics;
using Binance.Spot.Models;
using CryptoBot.Application.Binance.Contract.Interfaces;
using CryptoBot.CrossCutting.DTOs;
using Microsoft.Extensions.Logging;
using Refit;

namespace CryptoBot.Application.Binance.Client.Historical;

public class BinanceHistoricalClient : IBinanceHistoricalClient
{
    private readonly ILogger<BinanceHistoricalClient> _logger;
    private readonly IBinanceV3Client _binanceV3Client;

    public BinanceHistoricalClient(ILogger<BinanceHistoricalClient> logger)
    {
        _logger = logger;
        _binanceV3Client = RestService.For<IBinanceV3Client>("https://api.binance.com/api/v3");
    }

    private static long ConvertToUnixMilliseconds(DateTime date) => new DateTimeOffset(date).ToUnixTimeMilliseconds();

    private static long GetIntervalStepsMilliseconds(Interval interval, DateTime startTime, DateTime endTime)
    {
        var difference = endTime - startTime;

        if (interval == Interval.ONE_SECOND)
        {
            return difference.TotalMinutes > 5 ?
                5 * 60 * 1000 :
                Convert.ToInt64(difference.TotalMinutes * 60 * 1000);
        }

        throw new NotImplementedException("Interval steps to milliseconds not found");
    }

    public async Task<IEnumerable<KlineDto>> GetKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime) throw new ArgumentException("startTime argument cannot be greater than endTime argument");

        var (startTimeMs, endTimeMs) = (ConvertToUnixMilliseconds(startTime), ConvertToUnixMilliseconds(endTime));

        var timer = new Stopwatch();
        timer.Start();

        var ranges = new SortedList<long, long>();
        var stepsSize = GetIntervalStepsMilliseconds(interval, startTime, endTime);
        for (var i = startTimeMs; i < endTimeMs - stepsSize; i += stepsSize)
        {
            ranges.Add(i, i + stepsSize);
        }

        timer.Stop();
        _logger.LogInformation($"Create range time elapsed = {timer.Elapsed}");

        timer.Restart();

        var requests = new List<Task<IEnumerable<IEnumerable<dynamic>>>>();
        foreach (var (start, end) in ranges)
        {
            requests.Add(_binanceV3Client.GetKlines(symbol, interval, start, end));
        }

        timer.Stop();
        _logger.LogInformation($"Create requests time elapsed = {timer.Elapsed}");

        timer.Restart();

        var klinesList = (await Task.WhenAll(requests.ToArray())).SelectMany(s => s).ToList();
        var klines = klinesList.SelectMany(s => new List<KlineDto>()
        {
            new()
            {
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(s.ElementAt(0)).DateTime,
                OpenPrice = Convert.ToDouble(s.ElementAt(1)),
                HighPrice = Convert.ToDouble(s.ElementAt(2)),
                LowPrice = Convert.ToDouble(s.ElementAt(3)),
                ClosePrice = Convert.ToDouble(s.ElementAt(4)),
                Volume = Convert.ToDouble(s.ElementAt(5)),
                CloseTime = DateTimeOffset.FromUnixTimeMilliseconds(s.ElementAt(6)).DateTime,
                QuoteAssetVolume = Convert.ToDouble(s.ElementAt(7)),
                NumberOfTrades = s.ElementAt(8),
                TakerBuyBaseAssetVolume = Convert.ToDouble(s.ElementAt(9)),
                TakerBuyQuoteAssetVolume = Convert.ToDouble(s.ElementAt(10)),
            }
        });

        timer.Stop();
        _logger.LogInformation($"Awaiting requests time elapsed = {timer.Elapsed}");

        return klines;
    }
}