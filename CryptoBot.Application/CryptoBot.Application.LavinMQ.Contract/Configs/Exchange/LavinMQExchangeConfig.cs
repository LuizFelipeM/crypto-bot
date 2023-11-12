namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQExchangeConfig : LavinMQBaseConfig
{
    public required string Type { get; set; }
    public IDictionary<string, object>? Arguments { get; set; }
}