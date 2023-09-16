using CryptoBot.Application.Binance.Client;
using CryptoBot.Application.Binance.Client.API;
using CryptoBot.Domain.Interfaces.Repositories;
using CryptoBot.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class TraderController : ControllerBase
{
    private readonly ILogger<TraderController> _logger;
    private readonly IBinanceMarketClient _binanceMarketClient;
    private readonly IBinanceSpotClient _binanceSpotClient;
    private readonly IOrderRepository _orderRepository;

    public TraderController(
        ILogger<TraderController> logger,
        IBinanceMarketClient binanceMarketClient,
        IBinanceSpotClient binanceSpotClient,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _binanceMarketClient = binanceMarketClient;
        _binanceSpotClient = binanceSpotClient;
        _orderRepository = orderRepository;
    }

    [HttpGet("subscribe")]
    public async Task Subscribe()
    {
        await _binanceMarketClient.Subscribe("bnbbtc@trade", async data => _logger.LogInformation(data));
    }

    [HttpPost("order")]
    public async Task<string> NewOrder([FromBody] NewOrderDto newOrder)
    {
        return await _binanceSpotClient.NewOrder(newOrder);
    }

    [HttpPost("repo")]
    public void Save()
    {
        _orderRepository.Save(new Order
        {
            Price = 10,
            Quantity = 2,
            Side = CrossCutting.Enums.Side.BUY
        });
    }

    [HttpGet("repo")]
    public IEnumerable<Order> GetAll()
    {
        return _orderRepository.GetAll();
    }
}
