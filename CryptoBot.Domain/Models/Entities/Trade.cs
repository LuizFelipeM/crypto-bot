using SQLite;

namespace CryptoBot.Domain.Models;

[Table("trades")]
public class Trade
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}