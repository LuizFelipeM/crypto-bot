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

        if (interval == Interval.ONE_MINUTE)
        {
            return 1000 * 60 * 1000;
        }

        throw new NotImplementedException("Interval steps to milliseconds not found");
    }

    public async Task<IEnumerable<KlineDto>> GetKlines(string symbol, Interval interval, DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime) throw new ArgumentException("startTime argument cannot be greater than endTime argument");

        var (startTimeMs, endTimeMs) = (ConvertToUnixMilliseconds(startTime), ConvertToUnixMilliseconds(endTime));

        // var timer = new Stopwatch();
        // timer.Start();

        // var ranges = new SortedList<long, long>();
        // var stepsSize = GetIntervalStepsMilliseconds(interval, startTime, endTime);
        // for (var i = startTimeMs; i < endTimeMs - stepsSize; i += stepsSize)
        // {
        //     ranges.Add(i, i + stepsSize);
        // }

        // timer.Stop();
        // _logger.LogInformation($"Create range time elapsed = {timer.Elapsed}");

        // timer.Restart();

        // // var requests = new List<Task<IEnumerable<IEnumerable<dynamic>>>>();
        // var requests = new List<Task<IEnumerable<IEnumerable<dynamic>>>>();
        // foreach (var (start, end) in ranges)
        // {
        //     requests.Add(_binanceV3Client.GetKlines(symbol, interval, start, end));
        // }

        // timer.Stop();
        // _logger.LogInformation($"Create requests time elapsed = {timer.Elapsed}");

        // timer.Restart();

        var limit = Convert.ToInt32(2.628e+6);
        var test = await _binanceV3Client.GetKlines(symbol, interval, startTimeMs, endTimeMs, limit: limit);

        // var requestBatchSize = 25;
        var klines = new List<KlineDto>();
        // for (int i = 0; i < requests.Count; i += requestBatchSize)
        // {
        //     var klinesList = (await Task.WhenAll(requests.Skip(i).Take(requestBatchSize).ToArray())).SelectMany(s => s).ToList();
        //     klines.AddRange(
        //         klinesList.SelectMany(s => new List<KlineDto>()
        //         {
        //             new()
        //             {
        //                 OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(s.ElementAt(0)).DateTime,
        //                 OpenPrice = Convert.ToDouble(s.ElementAt(1)),
        //                 HighPrice = Convert.ToDouble(s.ElementAt(2)),
        //                 LowPrice = Convert.ToDouble(s.ElementAt(3)),
        //                 ClosePrice = Convert.ToDouble(s.ElementAt(4)),
        //                 Volume = Convert.ToDouble(s.ElementAt(5)),
        //                 CloseTime = DateTimeOffset.FromUnixTimeMilliseconds(s.ElementAt(6)).DateTime,
        //                 QuoteAssetVolume = Convert.ToDouble(s.ElementAt(7)),
        //                 NumberOfTrades = s.ElementAt(8),
        //                 TakerBuyBaseAssetVolume = Convert.ToDouble(s.ElementAt(9)),
        //                 TakerBuyQuoteAssetVolume = Convert.ToDouble(s.ElementAt(10)),
        //             }
        //         }));
        //     Thread.Sleep(1000);
        // }

        // timer.Stop();
        // _logger.LogInformation($"Awaiting requests time elapsed = {timer.Elapsed}");

        return klines;
    }

    public async IAsyncEnumerable<IEnumerable<KlineDto>> GetKlinesYield(string symbol, Interval interval, DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime) throw new ArgumentException("startTime argument cannot be greater than endTime argument");

        // var (startTimeMs, endTimeMs) = (ConvertToUnixMilliseconds(startTime), ConvertToUnixMilliseconds(endTime));

        for (DateTime i = startTime; i < endTime; i = i.AddMonths(1))
            yield return (await _binanceV3Client.GetKlines(symbol,
                                                           interval,
                                                           startTime: ConvertToUnixMilliseconds(i),
                                                           endTime: ConvertToUnixMilliseconds(i.AddMonths(1)),
                                                           limit: Convert.ToInt32(2.628e+6)))
                                                    .SelectMany(s => new List<KlineDto>()
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
    }
}