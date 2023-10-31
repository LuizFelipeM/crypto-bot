using CryptoBot.Application.LavinMQ.Contract.Attributes;
using Newtonsoft.Json;

namespace CryptoBot.Infrastructure.Service.Contracts;

[RoutingKey("kline")]
public class KlineContract
{
    [JsonProperty("O")]
    public required DateTime OpenTime { get; set; }

    [JsonProperty("o")]
    public required double OpenPrice { get; set; }

    [JsonProperty("h")]
    public required double HighPrice { get; set; }

    [JsonProperty("l")]
    public required double LowPrice { get; set; }

    [JsonProperty("c")]
    public required double ClosePrice { get; set; }

    [JsonProperty("v")]
    public required double Volume { get; set; }

    [JsonProperty("C")]
    public required DateTime CloseTime { get; set; }

    [JsonProperty("Q")]
    public required double QuoteAssetVolume { get; set; }

    [JsonProperty("n")]
    public required long NumberOfTrades { get; set; }

    [JsonProperty("b")]
    public required double TakerBuyBaseAssetVolume { get; set; }

    [JsonProperty("q")]
    public required double TakerBuyQuoteAssetVolume { get; set; }
}