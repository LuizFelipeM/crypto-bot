namespace CryptoBot.Application.Job;

public class JobConfig
{
    public required string Cron { get; set; }
    public required bool Active { get; set; }
}