using CryptoBot.Application.Binance.Client;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class TraderController : ControllerBase
{
    private readonly ILogger<TraderController> _logger;
    private readonly IBinanceClient _binanceClient;

    public TraderController(ILogger<TraderController> logger, IBinanceClient binanceClient)
    {
        _logger = logger;
        _binanceClient = binanceClient;
    }

    [HttpGet("start")]
    public async Task Start()
    {
        await _binanceClient.Start();
    }
}
