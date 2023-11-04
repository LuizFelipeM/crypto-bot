namespace CryptoBot.Infrastructure.Repository.MySql;

public class MySqlConfig
{
    public required string User { get; set; }
    public required string Password { get; set; }
    public required string RawConnectionString { get; set; }

    public string ConnectionString => RawConnectionString.Replace("<User>", User).Replace("<Password>", Password);
}
