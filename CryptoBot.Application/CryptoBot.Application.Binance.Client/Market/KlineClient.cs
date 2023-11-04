using Binance.Spot;
using Binance.Spot.Models;
using CryptoBot.Application.Binance.Client.Mappers;
using CryptoBot.Application.Binance.Contract.DTOs.Streams;
using CryptoBot.Domain;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Client.Market;

public class KlineClient : IKlineClient
{
    private readonly Dictionary<string, HashSet<Interval>> _watchingSymbols = new();
    private readonly ILogger<KlineClient> _logger;
    private MarketDataWebSocket? _webSocket;

    public event KlineReceived? OnKlineReceived;
    public event KlineErrorReceived? OnKlineErrorReceived;
    public event Disconnection? OnDisconnection;

    public KlineClient(ILogger<KlineClient> logger)
    {
        _logger = logger;
    }

    private IEnumerable<string> GetStreams()
    {
        foreach (var (symbol, intervals) in _watchingSymbols)
            foreach (var interval in intervals)
                yield return $"{symbol}@kline_{interval}";
    }

    private async Task Connect()
    {
        if (_webSocket != null) Disconnect();

        var streams = GetStreams().ToArray();
        _webSocket = new MarketDataWebSocket(streams: streams.ToArray());

        _webSocket.OnMessageReceived(async (data) => await Task.Run(() =>
        {
            Stream<KlineStream>? payload = null;

            try
            {
                payload = JsonConvert.DeserializeObject<Stream<KlineStream>>(data);
            }
            catch (Exception ex)
            {
                if (OnKlineErrorReceived != null)
                {
                    KlineErrorReceived callback = new(OnKlineErrorReceived);
                    callback(ex);
                }
            }

            try
            {
                if (OnKlineReceived != null && payload != null)
                {
                    KlineReceived callback = new(OnKlineReceived);
                    var klineEvent = TinyMapper.Map<KlineEvent>(payload.Data.Kline);
                    callback(klineEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"OnMessageReceived error while calling KlineReceived event");
            }
        }), CancellationToken.None);
        await _webSocket.ConnectAsync(CancellationToken.None);
    }

    private void Disconnect()
    {
        _webSocket?.Dispose();
        if (OnDisconnection != null)
            new Disconnection(OnDisconnection)();
    }

    /// <summary>
    /// Add the symbol and interval to the watchingSymbols holder
    /// </summary>
    /// <param name="symbol">Symbol of base and quote asset</param>
    /// <param name="interval">Desired interval</param>
    /// <returns>If added successfuly</returns>
    private bool AddToWatchingSymbols(string symbol, Interval interval)
    {
        if (_watchingSymbols.TryGetValue(symbol, out var intervals))
            return intervals.Add(interval);
        else
            return _watchingSymbols.TryAdd(symbol, new() { interval });
    }

    public async Task Watch(string[] symbols, Domain.Models.Types.Interval interval)
    {
        var reconnect = false;

        foreach (var symbol in symbols)
            reconnect = AddToWatchingSymbols(symbol.ToLowerInvariant(), Mapper.Map(interval));

        if (reconnect) await Connect();
    }

    public async Task Watch(string symbol, Domain.Models.Types.Interval interval)
    {
        if (AddToWatchingSymbols(symbol.ToLowerInvariant(), Mapper.Map(interval)))
            await Connect();
    }
}
