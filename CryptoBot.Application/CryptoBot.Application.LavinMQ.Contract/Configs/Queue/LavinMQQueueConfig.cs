using CryptoBot.Application.LavinMQ.Contract.Configs.Queue;

namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQQueueConfig : LavinMQBaseConfig
{
    public bool Exclusive { get; set; } = false;
    public LavinMQFeaturesConfig? Features { get; set; }
    public List<LavinMQBindQueueConfig> Bindings { get; set; } = new();
    public LavinMQQueueConfig? DeadLetterQueue { get; set; }
}
