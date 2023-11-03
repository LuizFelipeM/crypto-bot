using Binance.Spot.Models;
using CryptoBot.CrossCutting.JsonConverters;
using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Contract;

public class Kline
{
    /// <summary>
    /// Kline start time
    /// </summary>
    [JsonProperty("t")]
    public required long OpenTime { get; set; }

    /// <summary>
    /// Kline close time
    /// </summary>
    [JsonProperty("T")]
    public required long CloseTime { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("s")]
    public required string Symbol { get; set; }

    /// <summary>
    /// Interval
    /// </summary>    
    [JsonProperty("i")]
    public required string Interval { get; set; }

    /// <summary>
    /// First trade ID
    /// </summary>    
    [JsonProperty("f")]
    public required long FirstTradeId { get; set; }

    /// <summary>
    /// Last trade ID
    /// </summary>    
    [JsonProperty("L")]
    public required long LastTradeId { get; set; }

    /// <summary>
    /// Open price
    /// </summary>    
    [JsonProperty("o")]
    // required Need a string to double conversion
    public double OpenPrice { get; set; }

    /// <summary>
    /// Close price
    /// </summary>    
    [JsonProperty("c")]
    // required Need a string to double conversion
    public double ClosePrice { get; set; }

    /// <summary>
    /// High price
    /// </summary>    
    [JsonProperty("h")]
    // required Need a string to double conversion
    public double HighPrice { get; set; }

    /// <summary>
    /// Low price
    /// </summary>    
    [JsonProperty("l")]
    // required Need a string to double conversion
    public double LowPrice { get; set; }

    /// <summary>
    /// Base asset volume
    /// </summary>    
    [JsonProperty("v")]
    // required Need a string to double conversion
    public double BaseAssetVolume { get; set; }

    /// <summary>
    /// Number of trades
    /// </summary>    
    [JsonProperty("n")]
    public required long NumberOfTrades { get; set; }

    /// <summary>
    /// Is this kline closed?
    /// </summary>    
    [JsonProperty("x")]
    public required bool IsKlineClosed { get; set; }

    /// <summary>
    /// Quote asset volume
    /// </summary>    
    [JsonProperty("q")]
    // required Need a string to double conversion
    public double QuoteAssetVolume { get; set; }

    /// <summary>
    /// Taker buy base asset volume
    /// </summary>    
    [JsonProperty("V")]
    // required Need a string to double conversion
    public double TakerBuyBaseAssetVolume { get; set; }

    /// <summary>
    /// Taker buy quote asset volume
    /// </summary>    
    [JsonProperty("Q")]
    // required Need a string to double conversion
    public double TakerBuyQuoteAssetVolume { get; set; }

    /// Ignore
    // [JsonProperty("B")]
    // public required string Ignore { get; set; }
}
