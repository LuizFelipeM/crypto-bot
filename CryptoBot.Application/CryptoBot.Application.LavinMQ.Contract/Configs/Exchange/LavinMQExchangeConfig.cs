namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQExchangeConfig
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
    public bool ForceDelete { get; set; } = false;
    public IDictionary<string, object>? Arguments { get; set; }
}