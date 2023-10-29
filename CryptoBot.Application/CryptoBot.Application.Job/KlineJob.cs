using CryptoBot.CrossCutting.DTOs;
using Microsoft.Extensions.Logging;

namespace CryptoBot.Application.Job;

public class KlineJob : LavinMQConsumerJob<KlineContract>
{
    public KlineJob(
        ILogger<LavinMQConsumerJob<KlineContract>> logger,
        IServiceProvider serviceProvider
    ) : base(logger, serviceProvider)
    {
    }
}
