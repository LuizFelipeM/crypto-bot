using CryptoBot.CrossCutting.DTOs;
using CryptoBot.Infrastructure.Service.Interfaces.Historical;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoricalController : ControllerBase
{
    private readonly ILogger<HistoricalController> _logger;
    private readonly IHistoricalService _historicalService;

    public HistoricalController(
        ILogger<HistoricalController> logger,
        IHistoricalService historicalService)
    {
        _logger = logger;
        _historicalService = historicalService;
    }

    [HttpGet("klines")]
    public IEnumerable<KlineDto> GetKlines([FromQuery] string symbol, [FromQuery] int startYear, [FromQuery] int startMonth, [FromQuery] int startDay, [FromQuery] int endYear, [FromQuery] int endMonth, [FromQuery] int endDay)
    {
        // DateTime startTime = new(startYear, startMonth, startDay);
        // DateTime endTime = new(endYear, endMonth, endDay);
        // var a = _historicalService.GetKlines(symbol, Interval.ONE_SECOND, startTime, endTime).Result;
        // var bSize = a.GetSize();
        // return a;
        return new List<KlineDto>();
    }

    [HttpPost("publish/kline")]
    public void PublishKline()
    {
        var rand = new Random();
        _historicalService.Publish(new()
        {
            OpenTime = DateTime.Now.AddSeconds(-1),
            OpenPrice = rand.NextDouble() * 100,
            HighPrice = rand.NextDouble() * 100,
            LowPrice = rand.NextDouble() * 100,
            ClosePrice = rand.NextDouble() * 100,
            Volume = rand.Next(),
            CloseTime = DateTime.Now,
            QuoteAssetVolume = rand.NextDouble() * 100,
            NumberOfTrades = rand.Next(),
            TakerBuyBaseAssetVolume = rand.NextDouble() * 100,
            TakerBuyQuoteAssetVolume = rand.NextDouble() * 100,
        })
        .Wait();
    }
}