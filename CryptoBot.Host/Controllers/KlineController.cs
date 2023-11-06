using CryptoBot.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class KlineController : ControllerBase
{
    private readonly ILogger<KlineController> _logger;
    private readonly IKlineService _klineService;

    public KlineController(
        ILogger<KlineController> logger,
        IKlineService klineService)
    {
        _logger = logger;
        _klineService = klineService;
    }

    [HttpPost("tracking/start/btc")]
    public void StartTrackingBtc() => _klineService.StartTrackingBtc();

    [HttpPost("tracking/start/usdt")]
    public void StartTrackingUsdt() => _klineService.StartTrackingUsdt();

    [HttpPost("tracking/stop")]
    public void StopTrackint() => _klineService.StopTracking();
}