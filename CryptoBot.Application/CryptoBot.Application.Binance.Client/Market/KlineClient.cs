using Binance.Spot;
using Binance.Spot.Models;
using CryptoBot.Application.Binance.Contract;
using CryptoBot.Application.Binance.Contract.DTOs.Streams;
using Newtonsoft.Json;

namespace CryptoBot.Application.Binance.Client.Market;

internal class KlineClient
{
    private readonly Dictionary<string, HashSet<Interval>> _watchingSymbols = new();
    private MarketDataWebSocket? _webSocket;

    internal delegate void KlineReceived(Kline kline);
    internal event KlineReceived? OnKlineReceived;

    internal delegate void KlineErrorReceived(Exception exception);
    internal event KlineErrorReceived? OnKlineErrorReceived;

    internal delegate void Disconnection();
    internal event Disconnection? OnDisconnection;

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

        _webSocket.OnMessageReceived(async (data) =>
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
                    callback(payload.Data.Kline);
                }
            }
            catch (Exception)
            {
            }
        }, CancellationToken.None);
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

    public async Task Watch(string[] symbols, Interval interval)
    {
        var reconnect = false;

        foreach (var symbol in symbols)
            reconnect = AddToWatchingSymbols(symbol.ToLowerInvariant(), interval);

        if (reconnect) await Connect();
    }

    public async Task Watch(string symbol, Interval interval)
    {
        if (AddToWatchingSymbols(symbol.ToLowerInvariant(), interval))
            await Connect();
    }
}
