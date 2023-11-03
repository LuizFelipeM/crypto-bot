using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Contract;

public class DataStream
{
    [JsonProperty("e")]
    public required string EventType { get; set; }

    [JsonProperty("E")]
    public required long EventTime { get; set; }

    [JsonProperty("s")]
    public required string Symbol { get; set; }
}
