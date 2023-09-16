using System.Text.Json.Serialization;
using Binance.Spot.Models;
using CryptoBot.CrossCutting.Enums;
using CryptoBot.CrossCutting.JsonConverters;
using Side = Binance.Spot.Models.Side;

namespace CryptoBot.Application.Binance.Client.API;

public class NewOrderDto
{
    public required string Symbol { get; set; }
    [JsonConverter(typeof(StructJsonConverter<Side>))]
    public required Side Side { get; set; }
    [JsonConverter(typeof(StructJsonConverter<OrderType>))]
    public required OrderType Type { get; set; }
    [JsonConverter(typeof(StructJsonConverter<TimeInForce>))]
    public TimeInForce? TimeInForce { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? QuoteOrderQty { get; set; }
    public decimal? Price { get; set; }
    public string? NewClientOrderId { get; set; }
    public int? StrategyId { get; set; }
    public int? StrategyType { get; set; }
    public decimal? StopPrice { get; set; }
    public decimal? TrailingDelta { get; set; }
    public decimal? IcebergQty { get; set; }
    [JsonConverter(typeof(StructJsonConverter<NewOrderResponseType>))]
    public NewOrderResponseType? NewOrderRespType { get; set; }
    public long? RecvWindow { get; set; }
}