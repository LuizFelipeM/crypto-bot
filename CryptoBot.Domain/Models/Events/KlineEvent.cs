namespace CryptoBot.Domain;

public class KlineEvent
{
    public required long OpenTime { get; set; }
    public required long CloseTime { get; set; }
    public required string Symbol { get; set; }
    public required string Interval { get; set; }
    public long FirstTradeId { get; set; }
    public long LastTradeId { get; set; }
    public required double OpenPrice { get; set; }
    public required double ClosePrice { get; set; }
    public required double HighPrice { get; set; }
    public required double LowPrice { get; set; }
    public required double BaseAssetVolume { get; set; }
    public required long NumberOfTrades { get; set; }
    public required bool IsKlineClosed { get; set; }
    public required double QuoteAssetVolume { get; set; }
    public required double TakerBuyBaseAssetVolume { get; set; }
    public required double TakerBuyQuoteAssetVolume { get; set; }
}
