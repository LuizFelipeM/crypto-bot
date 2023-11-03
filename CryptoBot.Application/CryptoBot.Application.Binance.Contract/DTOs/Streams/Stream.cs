using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Contract.DTOs.Streams;

public class Stream<T>
    where T : DataStream
{
    [JsonProperty("data")]
    public required T Data { get; set; }
}
