using CryptoBot.Application.Binance.Contract;
using CryptoBot.Domain;
using CryptoBot.Domain.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using Newtonsoft.Json.Linq;

namespace CryptoBot.Infrastructure.Service.Observers;

[KlineSymbols(Symbol.ONE_INCH, Symbol.USDT)]
public class OneInchUsdtObserver : IObserver<KlineEvent>
{
    private readonly ILogger<IObserver<KlineEvent>> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OneInchUsdtObserver(
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
            if (!kline.IsKlineClosed) return;

            var scope = _serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IKlineRepository>();
            repository.Upsert(TinyMapper.Map<KlineEntity>(kline));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{GetType().Name} OnNext error while processing {JObject.FromObject(kline)}");
        }
    }
}
