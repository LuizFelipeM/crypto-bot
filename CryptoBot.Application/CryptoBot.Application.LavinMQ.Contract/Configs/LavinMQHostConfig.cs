namespace CryptoBot.Application.LavinMQ.Contract.Configs;

public class LavinMQHostConfig
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required int Port { get; set; }
    public string? HostName { get; set; }
    public bool DispatchConsumersAsync { get; set; } = true;
    public string VirtualHost { get; set; } = "%2F";
    public TimeSpan RequestedHeartbeat { get; set; } = new TimeSpan(0, 0, 15);
    public bool AutomaticRecoveryEnabled { get; set; } = true;
    public bool SslEnabled { get; set; } = false;
}
