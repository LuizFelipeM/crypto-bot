using CryptoBot.Infrastructure.Service.Contracts;
using Microsoft.Extensions.Logging;

namespace CryptoBot.Infrastructure.Job;

public class KlineJob : LavinMQConsumerJob<KlineContract>
{
    public KlineJob(
        ILogger<LavinMQConsumerJob<KlineContract>> logger,
        IServiceProvider serviceProvider
    ) : base(logger, serviceProvider)
    {
    }
}
