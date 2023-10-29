using CryptoBot.Application.LavinMQ.Contract.Configs;

namespace CryptoBot.Host.Configs.Entities;

public class LavinMQInfrastructureConfigs
{
    public required Dictionary<string, LavinMQExchangeConfig> Exchanges { get; set; }
    public required Dictionary<string, LavinMQQueueConfig> Queues { get; set; }
}