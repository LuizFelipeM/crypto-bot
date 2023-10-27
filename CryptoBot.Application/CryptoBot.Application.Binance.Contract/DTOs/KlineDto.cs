namespace CryptoBot.Application.Binance.Contract.DTOs;

public class KlineDto
{
    // "open time",
    public required DateTime OpenTime { get; set; }
    // "open price",
    public required double OpenPrice { get; set; }
    // "high price",
    public required double HighPrice { get; set; }
    // "low price",
    public required double LowPrice { get; set; }
    // "close price",
    public required double ClosePrice { get; set; }
    // "volume",
    public required double Volume { get; set; }
    // "close time",
    public required DateTime CloseTime { get; set; }
    // "quote asset volume",
    public required double QuoteAssetVolume { get; set; }
    // "number of trades",
    public required long NumberOfTrades { get; set; }
    // "taker buy base asset volume",
    public required double TakerBuyBaseAssetVolume { get; set; }
    // "taker buy quote asset volume",
    public required double TakerBuyQuoteAssetVolume { get; set; }
}