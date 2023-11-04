using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Services.Base;
using CryptoBot.Domain.Interfaces.Services.Observables;
using CryptoBot.Domain.Models.Entities;
using CryptoBot.Domain.Models.Types;
using CryptoBot.Infrastructure.Service.Observers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CryptoBot.Infrastructure.Service;

public class KlineService : Service<KlineEntity, long>, IKlineService
{
    private readonly ILogger<KlineService> _logger;
    private readonly IKlineRepository _klineRepository;
    private readonly IKlineObservable _klineObservable;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private static List<IDisposable> _unsubscribers = new();

    public KlineService(
        ILogger<KlineService> logger,
        IKlineRepository klineRepository,
        IKlineObservable klineObservable,
        IServiceScopeFactory serviceScopeFactory) : base(klineRepository)
    {
        _logger = logger;
        _klineRepository = klineRepository;
        _klineObservable = klineObservable;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartTrackingBtc()
    {
        try
        {
            var scope = _serviceScopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IObserver<KlineEvent>>>();
            var unsubscriber = await _klineObservable.Subscribe(Interval.ONE_MINUTE,
                                                                new BtcUsdtObserver(logger, _serviceScopeFactory));
            _unsubscribers.Add(unsubscriber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StartTracking subscribe failed");
            throw;
        }
    }


    public async Task StartTrackingUsdt()
    {
        try
        {
            var scope = _serviceScopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IObserver<KlineEvent>>>();
            var unsubscriber = await _klineObservable.Subscribe(Interval.ONE_MINUTE,
                                                                new OneInchUsdtObserver(logger, _serviceScopeFactory));
            _unsubscribers.Add(unsubscriber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StartTracking subscribe failed");
            throw;
        }
    }

    public void StopTracking()
    {
        try
        {
            foreach (var unsubscriber in _unsubscribers)
                unsubscriber.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StopTracking unsubscribe failed");
            throw;
        }
    }
}
