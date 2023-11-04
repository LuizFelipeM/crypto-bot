using CryptoBot.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Host.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpPost("tracking/start")]
    public void StartTracking() => _klineService.StartTracking();

    [HttpPost("tracking/stop")]
    public void StopTrackint() => _klineService.StopTrackint();
}