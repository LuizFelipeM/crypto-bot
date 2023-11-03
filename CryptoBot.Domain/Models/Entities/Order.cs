using CryptoBot.CrossCutting.Enums;
using SQLite;

namespace CryptoBot.Domain.Models;

[Table("orders")]
public class Order
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public Side Side { get; set; }
    public double Price { get; set; }
    public double Quantity { get; set; }
}
