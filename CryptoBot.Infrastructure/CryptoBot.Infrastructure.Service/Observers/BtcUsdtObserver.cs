using CryptoBot.Application.Binance.Contract;
using CryptoBot.Domain;
using CryptoBot.Domain.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using Newtonsoft.Json.Linq;

namespace CryptoBot.Infrastructure.Service.Observers;

[KlineSymbols(Symbol.BTC, Symbol.USDT)]
public class BtcUsdtObserver : IObserver<KlineEvent>
{
    private readonly ILogger<IObserver<KlineEvent>> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BtcUsdtObserver(
        ILogger<IObserver<KlineEvent>> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void OnCompleted() => throw new NotImplementedException();
    public void OnError(Exception error) => _logger.LogError(error, $"{GetType().Name} OnError called");

    public void OnNext(KlineEvent kline)
    {
        try
        {
            var scope = _serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IKlineRepository>();
            var entity = TinyMapper.Map<KlineEntity>(kline);
            repository.Upsert(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{GetType().Name} OnNext error while processing {JObject.FromObject(kline)}");
        }
    }
}
