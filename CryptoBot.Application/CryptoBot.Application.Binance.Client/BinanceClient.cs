namespace CryptoBot.Application.Binance.Client;

using CryptoBot.Application.Binance.Client.Streams;
using global::Binance.Common;
using global::Binance.Spot;
using global::Binance.Spot.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

public class BinanceClient : IBinanceClient
{
    private readonly ILogger<BinanceClient> _logger;
    private readonly MarketDataWebSocket _websocket;

    public BinanceClient(ILogger<BinanceClient> logger)
    {
        _logger = logger;
        _websocket = new MarketDataWebSocket("btcusdt@trade");
    }

    public async Task Start()
    {
        _websocket.OnMessageReceived((data) =>
        {
            if (string.IsNullOrEmpty(data)) return Task.CompletedTask;

            try
            {
                var tradeStreams = JObject.Parse(data).ToObject<TradeStreams>();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{GetType().Name} {JObject.FromObject(ex)}");
                return Task.FromException(ex);
            }

        },
        CancellationToken.None);

        await _websocket.ConnectAsync(CancellationToken.None);
    }
}
