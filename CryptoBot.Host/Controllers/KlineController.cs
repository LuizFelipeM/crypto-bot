using Binance.Spot.Models;
using CryptoBot.CrossCutting.DTOs;
using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Services;
using CryptoBot.Domain.Models.Entities;
using CryptoBot.Domain.Models.Types;
using Microsoft.AspNetCore.Mvc;
using Interval = CryptoBot.Domain.Models.Types.Interval;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class KlineController : ControllerBase
{
    private readonly ILogger<KlineController> _logger;
    private readonly IHistoricalService _historicalService;
    private readonly IKlineRepository _klineRepository;

    public KlineController(
        ILogger<KlineController> logger,
        IHistoricalService historicalService,
        IKlineRepository klineRepository)
    {
        _logger = logger;
        _historicalService = historicalService;
        _klineRepository = klineRepository;
    }

    [HttpGet("subscribe/btc")]
    public void SubscribeBtc() => _historicalService.SubscribeBtc();

    [HttpGet("subscribe/usdt")]
    public void SubscribeUsdt() => _historicalService.SubscribeUsdt();

    [HttpGet]
    public IEnumerable<KlineEntity> GetAll() => _klineRepository.GetAll();

    [HttpGet("{id}")]
    public KlineEntity? Find([FromRoute] long id) => _klineRepository.Find(id);

    [HttpPost]
    public void Insert([FromBody] KlineEntity entity) => _klineRepository.Insert(entity);

    [HttpDelete("{id}")]
    public void Remove([FromRoute] long id)
    {
        var entity = _klineRepository.Find(id) ?? throw new Exception($"Kline with id {id} not found");
        _klineRepository.Remove(entity);
    }
}