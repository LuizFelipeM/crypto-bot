using Binance.Spot.Models;
using CryptoBot.Application.Binance.Contract;

namespace CryptoBot.Application.Binance.Client.Market;

public class KlineObservable : IKlineObservable
{
    /// <summary>
    /// Keep the last klines updates until its close
    /// </summary>
    private readonly HashSet<Kline> _klines = new();

    /// <summary>
    /// The observers of each symbol pair
    /// </summary>
    private readonly Dictionary<(string symbol, string interval), HashSet<IObserver<Kline>>> _symbols = new();
    private readonly KlineClient _marketClient = new();

    public KlineObservable()
    {
        _marketClient.OnKlineReceived += OnKlineReceived;
        _marketClient.OnKlineErrorReceived += OnKlineErrorReceived;
    }

    private Task AddSymbols(IEnumerable<KlineSymbolsAttribute> symbols, Interval interval) =>
        _marketClient.Watch(symbols.Select(s => s.Symbol).ToArray(), interval);
    private Task AddSymbol(KlineSymbolsAttribute symbols, Interval interval) =>
        _marketClient.Watch(symbols.Symbol, interval);

    private void OnKlineReceived(Kline kline)
    {
        if (kline.IsKlineClosed)
            _klines.RemoveWhere(k => k.Symbol == kline.Symbol);

        if (_symbols.TryGetValue((kline.Symbol.ToUpperInvariant(), kline.Interval), out var observers))
            foreach (var observer in observers)
                observer.OnNext(kline);
    }

    private void OnKlineErrorReceived(Exception exception)
    {
        foreach (var observer in _symbols.SelectMany(s => s.Value))
            observer.OnError(exception);
    }

    private static KlineSymbolsAttribute GetSymbolsAttribute(IObserver<Kline> observer)
    {
        var symbolsAttr = Attribute.GetCustomAttributes(observer.GetType()).FirstOrDefault(a => a is KlineSymbolsAttribute)
                          ?? throw new Exception($"Symbols not found on subscription, try using the SymbolsAttirbute");
        return (KlineSymbolsAttribute)symbolsAttr;
    }

    private IDisposable Subscribe(IObserver<Kline> observer, KlineSymbolsAttribute symbols, Interval interval)
    {
        var sendPreviousKlines = false;
        if (_symbols.TryGetValue((symbols.Symbol, interval), out var observers))
            sendPreviousKlines = observers.Add(observer);
        else
        {
            observers = new() { observer };
            _symbols.TryAdd((symbols.Symbol, interval), observers);
        }

        if (sendPreviousKlines)
            foreach (Kline kline in _klines.Where(k => k.Symbol == symbols.Symbol))
                observer.OnNext(kline);

        return new Unsubscriber<Kline>(observers, observer);
    }

    public async Task<IDisposable> Subscribe(IObserver<Kline> observer, Interval interval)
    {
        var symbols = GetSymbolsAttribute(observer);
        await AddSymbol(symbols, interval);
        return Subscribe(observer, symbols, interval);
    }

    public async Task<IEnumerable<IDisposable>> Subscribe(IEnumerable<IObserver<Kline>> observers, Interval interval)
    {
        var observersSymbols = observers.Select(o => new
        {
            Observer = o,
            Symbols = GetSymbolsAttribute(o)
        });
        await AddSymbols(observersSymbols.Select(o => o.Symbols), interval);
        return observersSymbols.Select(s => Subscribe(s.Observer, s.Symbols, interval));
    }
}
