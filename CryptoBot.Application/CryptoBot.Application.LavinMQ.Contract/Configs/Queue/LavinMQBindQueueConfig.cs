namespace CryptoBot.Application.LavinMQ.Contract.Configs.Queue;

public class LavinMQBindQueueConfig
{
    public required string ExchangeName { get; set; }
    public string? RoutingKey { get; set; }
}