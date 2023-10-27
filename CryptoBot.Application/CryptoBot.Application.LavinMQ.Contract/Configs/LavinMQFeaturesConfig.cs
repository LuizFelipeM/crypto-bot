namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQFeaturesConfig
{
    public required string DeadLetterExchange { get; set; }
    public string? DeadLetterRoutingKey { get; set; }
}