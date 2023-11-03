using SQLite;

namespace CryptoBot.Domain.Models;

[Table("candles")]
public class Candle
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime EventTime { get; set; }
    public required string Symbol { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime CloseTime { get; set; }
    public double OpenPrice { get; set; }
    public double ClosePrice { get; set; }
    public double HighPrice { get; set; }
    public double LowPrice { get; set; }
    public double BaseAssetVolume { get; set; }
    public int NumberOfTrades { get; set; }
    public bool IsThisKlineClosed { get; set; }
    public double QuoteAssetVolume { get; set; }
    public double TakerBuyBaseAssetVolume { get; set; }
    public double TakerBuyQuoteAssetVolume { get; set; }
}