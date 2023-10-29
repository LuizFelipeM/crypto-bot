
using CryptoBot.Application.LavinMQ.Contract.Configs.Queue;

namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQQueueConfig
{
    public required string Name { get; set; }
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public bool ForceDelete { get; set; } = false;
    public LavinMQFeaturesConfig? Features { get; set; }
    public List<LavinMQBindQueueConfig> Bindings { get; set; } = new();
    public LavinMQQueueConfig? DeadLetterQueue { get; set; }
}
