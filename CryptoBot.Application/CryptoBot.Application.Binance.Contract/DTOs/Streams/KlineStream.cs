using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Contract.DTOs.Streams
{
    public class KlineStream : DataStream
    {
        [JsonProperty("k")]
        public required Kline Kline { get; set; }
    }
}
