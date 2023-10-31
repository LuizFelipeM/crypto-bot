namespace CryptoBot.Infrastructure.Job;

public class JobConfig
{
    public required string Cron { get; set; }
    public required bool Active { get; set; }
}