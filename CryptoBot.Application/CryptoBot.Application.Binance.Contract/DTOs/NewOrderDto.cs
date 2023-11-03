using System.Text.Json.Serialization;
using Binance.Spot.Models;
using CryptoBot.CrossCutting.Enums;
using CryptoBot.CrossCutting.JsonConverters;
using Side = Binance.Spot.Models.Side;
using OrderType = Binance.Spot.Models.OrderType;
using TimeInForce = Binance.Spot.Models.TimeInForce;
using NewOrderResponseType = Binance.Spot.Models.NewOrderResponseType;

namespace CryptoBot.Application.Binance.Contract.DTOs;

public class NewOrderDto
{
    public required string Symbol { get; set; }
    [JsonConverter(typeof(StructPropertyNameJsonConverter<Side>))]
    public required Side Side { get; set; }
    [JsonConverter(typeof(StructPropertyNameJsonConverter<OrderType>))]
    public required OrderType Type { get; set; }
    [JsonConverter(typeof(StructPropertyNameJsonConverter<TimeInForce>))]
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
    [JsonConverter(typeof(StructPropertyNameJsonConverter<NewOrderResponseType>))]
    public NewOrderResponseType? NewOrderRespType { get; set; }
    public long? RecvWindow { get; set; }
}