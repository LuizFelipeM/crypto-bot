namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQBaseConfig
{
    public required string Name { get; set; }
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
    public bool RecereateOnStartup { get; set; } = false;
    public bool ForceDelete { get; set; } = false;
}