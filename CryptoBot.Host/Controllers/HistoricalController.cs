using Binance.Spot.Models;
using CryptoBot.Application.Binance.Contract.DTOs;
using CryptoBot.Application.Binance.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoricalController : ControllerBase
{
    private readonly ILogger<HistoricalController> _logger;
    private readonly IBinanceHistoricalClient _binanceHistoricalClient;

    public HistoricalController(
        ILogger<HistoricalController> logger,
        IBinanceHistoricalClient binanceHistoricalClient)
    {
        _logger = logger;
        _binanceHistoricalClient = binanceHistoricalClient;
    }

    [HttpGet("klines")]
    public IEnumerable<KlineDto> GetKlines([FromQuery] string symbol, [FromQuery] int startYear, [FromQuery] int startMonth, [FromQuery] int startDay, [FromQuery] int endYear, [FromQuery] int endMonth, [FromQuery] int endDay)
    {
        DateTime startTime = new(startYear, startMonth, startDay);
        DateTime endTime = new(endYear, endMonth, endDay);
        var a = _binanceHistoricalClient.GetKlines(symbol, Interval.ONE_SECOND, startTime, endTime).Result;
        var count = a.Count();
        return a;
    }
}