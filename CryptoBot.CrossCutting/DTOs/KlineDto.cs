using Newtonsoft.Json;

namespace CryptoBot.CrossCutting.DTOs;

public class KlineDto
{
    public required DateTime OpenTime { get; set; }
    public required double OpenPrice { get; set; }
    public required double HighPrice { get; set; }
    public required double LowPrice { get; set; }
    public required double ClosePrice { get; set; }
    public required double Volume { get; set; }
    public required DateTime CloseTime { get; set; }
    public required double QuoteAssetVolume { get; set; }
    public required long NumberOfTrades { get; set; }
    public required double TakerBuyBaseAssetVolume { get; set; }
    public required double TakerBuyQuoteAssetVolume { get; set; }
}