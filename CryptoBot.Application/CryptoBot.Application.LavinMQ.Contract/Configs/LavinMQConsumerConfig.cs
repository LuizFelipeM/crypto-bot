namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQConsumerConfig
{
    public required LavinMQQueueConfig Queue { get; set; }
    public ushort PrefetchCount { get; set; } = 1;
    public bool AutoAck { get; set; } = true;
    public bool NoLocal { get; set; } = false;
}