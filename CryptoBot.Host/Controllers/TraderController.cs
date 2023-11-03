using CryptoBot.Application.Binance.Contract.DTOs;
using CryptoBot.Application.Binance.Contract.Interfaces;
using CryptoBot.Domain.Interfaces.Repositories;
using CryptoBot.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class TraderController : ControllerBase
{
    private readonly ILogger<TraderController> _logger;
    private readonly IBinanceSpotClient _binanceSpotClient;
    private readonly IOrderRepository _orderRepository;

    public TraderController(
        ILogger<TraderController> logger,
        IBinanceSpotClient binanceSpotClient,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _binanceSpotClient = binanceSpotClient;
        _orderRepository = orderRepository;
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
