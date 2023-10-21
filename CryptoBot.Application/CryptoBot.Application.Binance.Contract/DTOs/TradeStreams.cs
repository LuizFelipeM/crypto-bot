using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Contract.DTOs;

public class TradeStreamDto
{
    [JsonProperty("e")]
    public required string EventType { get; set; }

    [JsonProperty("E")]
    public required long EventTime { get; set; }

    [JsonProperty("s")]
    public required string Symbol { get; set; }

    [JsonProperty("t")]
    public required long TradeId { get; set; }

    [JsonProperty("p")]
    public required double Price { get; set; }

    [JsonProperty("q")]
    public required string Quantity { get; set; }

    [JsonProperty("b")]
    public required long BuyerOrderId { get; set; }

    [JsonProperty("a")]
    public required long SellerOrderId { get; set; }

    [JsonProperty("T")]
    public required long TradeTime { get; set; }

    [JsonProperty("m")]
    public required bool IsTheBuyerTheMarketMaker { get; set; }
}