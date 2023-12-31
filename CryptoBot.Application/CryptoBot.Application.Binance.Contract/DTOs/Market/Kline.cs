﻿using CryptoBot.CrossCutting;
using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Contract;

public class Kline
{
    /// <summary>
    /// Kline start time
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(MillisecondsDateTimeJsonConverter))]
    public required DateTime OpenTime { get; set; }

    /// <summary>
    /// Kline close time
    /// </summary>
    [JsonProperty("T")]
    [JsonConverter(typeof(MillisecondsDateTimeJsonConverter))]
    public required DateTime CloseTime { get; set; }

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
    public double OpenPrice { get; set; }

    /// <summary>
    /// Close price
    /// </summary>    
    [JsonProperty("c")]
    public double ClosePrice { get; set; }

    /// <summary>
    /// High price
    /// </summary>    
    [JsonProperty("h")]
    public double HighPrice { get; set; }

    /// <summary>
    /// Low price
    /// </summary>    
    [JsonProperty("l")]
    public double LowPrice { get; set; }

    /// <summary>
    /// Base asset volume
    /// </summary>    
    [JsonProperty("v")]
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
    public double QuoteAssetVolume { get; set; }

    /// <summary>
    /// Taker buy base asset volume
    /// </summary>    
    [JsonProperty("V")]
    public double TakerBuyBaseAssetVolume { get; set; }

    /// <summary>
    /// Taker buy quote asset volume
    /// </summary>    
    [JsonProperty("Q")]
    public double TakerBuyQuoteAssetVolume { get; set; }
}
