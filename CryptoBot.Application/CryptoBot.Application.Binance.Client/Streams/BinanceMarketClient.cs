using Binance.Spot;
using Microsoft.Extensions.Logging;

namespace CryptoBot.Application.Binance.Client.Streams;

public class BinanceMarketClient : IBinanceMarketClient
{
    private readonly ILogger<BinanceMarketClient> _logger;
    private readonly BinanceConfig _config;
    private Dictionary<string, (MarketDataWebSocket webSocket, List<Func<string, Task>> subscribers)> symbolConnections = new();

    public BinanceMarketClient(
        ILogger<BinanceMarketClient> logger,
        BinanceConfig config)
    {
        _logger = logger;
        _config = config;
    }

    private async Task StartConnection(string stream, CancellationToken cancellationToken)
    {
        var webSocket = new MarketDataWebSocket(stream);
        symbolConnections.Add(stream, (webSocket, new()));

        webSocket.OnMessageReceived(async (data) =>
        {
            var (_, subscribers) = symbolConnections[stream];
            await Task.WhenAll(subscribers.Select(func => func.Invoke(data)));
        }, cancellationToken);

        await webSocket.ConnectAsync(cancellationToken);

        _logger.LogInformation($"{GetType().Name} connection started with stream {stream}");
    }

    public async Task Subscribe(string stream, Func<string, Task> onMessageReceived)
    {
        if (!symbolConnections.ContainsKey(stream)) await StartConnection(stream, CancellationToken.None);
        symbolConnections[stream].subscribers.Add(onMessageReceived);

        _logger.LogInformation($"{GetType().Name} subscribed to stream {stream} - subscribers count {symbolConnections[stream].subscribers.Count}");
    }
}