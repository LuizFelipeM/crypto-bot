using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CryptoBot.Infrastructure.Job;

[DisallowConcurrentExecution]
public abstract class LavinMQConsumerJob<T> : IJob
    where T : class
{
    private readonly ILogger<LavinMQConsumerJob<T>> _logger;
    private readonly IServiceProvider _serviceProvider;

    public LavinMQConsumerJob(
        ILogger<LavinMQConsumerJob<T>> logger,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public virtual async Task Execute(IJobExecutionContext context)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            using var receive = scope.ServiceProvider.GetRequiredService<ILavinMQConsumer<T>>();
            await receive.Consume();
            await Task.Delay(5000);
            while (receive.HasConsumer()) await Task.Delay(1000);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{GetType().Name} - Exception {ex}");
        }
    }
}