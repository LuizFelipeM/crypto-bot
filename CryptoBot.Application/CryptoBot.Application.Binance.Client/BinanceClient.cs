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
    private readonly WebSocketApi _websocket;

    public BinanceClient(
        ILogger<BinanceClient> logger,
        BinanceConfig config)
    {
        _logger = logger;
        _websocket = SetupWebSocket(config).Result;
    }

    public async Task<WebSocketApi> SetupWebSocket(BinanceConfig config)
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", config.ApiKey, new BinanceHmac(config.ApiSecret));
        websocket.OnMessageReceived(OnMessageReceived, CancellationToken.None);
        await websocket.ConnectAsync(CancellationToken.None);
        return websocket;
    }

    public async Task OnMessageReceived(string data)
    {
        try
        {
            if (string.IsNullOrEmpty(data)) return;

            var tradeStreams = JObject.Parse(data).ToObject<TradeStreams>();
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{GetType().Name} {JObject.FromObject(ex)}");
            await Task.FromException(ex);
        }
    }

    public async Task Start()
    {
        await _websocket.AccountTrade.NewOrderAsync(symbol: "BNBUSDT",
                                                    side: Side.BUY,
                                                    type: OrderType.LIMIT,
                                                    timeInForce: TimeInForce.GTC,
                                                    price: 300,
                                                    quantity: 1,
                                                    cancellationToken: CancellationToken.None);
        await Task.Delay(5000);
    }
}
