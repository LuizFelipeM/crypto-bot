using CryptoBot.Application.Binance.Contract;
using CryptoBot.CrossCutting.Utils;
using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Services.Observables;
using CryptoBot.Domain.Models.Types;

namespace CryptoBot.Infrastructure.Service.Observables;

public class KlineObservable : IKlineObservable
{
    /// <summary>
    /// Keep the last klines updates until its close
    /// </summary>
    private readonly HashSet<KlineEvent> _klines = new();

    /// <summary>
    /// The observers of each symbol pair
    /// </summary>
    private readonly Dictionary<(string symbol, string interval), HashSet<IObserver<KlineEvent>>> _symbols = new();
    private readonly IKlineClient _klineClient;

    public KlineObservable(IKlineClient klineClient)
    {
        _klineClient = klineClient;
        _klineClient.OnKlineReceived += OnKlineReceived;
        _klineClient.OnKlineErrorReceived += OnKlineErrorReceived;
    }

    private Task AddSymbols(IEnumerable<KlineSymbolsAttribute> symbols, Interval interval) =>
        _klineClient.Watch(symbols.Select(s => s.AggregatedSymbols).ToArray(), interval);
    private Task AddSymbol(KlineSymbolsAttribute symbols, Interval interval) =>
        _klineClient.Watch(symbols.AggregatedSymbols, interval);

    private void OnKlineReceived(KlineEvent kline)
    {
        if (kline.IsKlineClosed)
            _klines.RemoveWhere(k => k.Symbol == kline.Symbol);

        if (_symbols.TryGetValue((kline.Symbol.ToUpperInvariant(), kline.Interval), out var observers))
            observers.AsParallel()
                .WithDegreeOfParallelism(50)
                .ForAll(observer => observer.OnNext(kline));
    }

    private void OnKlineErrorReceived(Exception exception)
    {
        _symbols.SelectMany(s => s.Value)
            .AsParallel()
            .WithDegreeOfParallelism(50)
            .ForAll(observer => observer.OnError(exception));
    }

    private static KlineSymbolsAttribute GetSymbolsAttribute(IObserver<KlineEvent> observer) =>
        observer.GetCustomAttributes<KlineSymbolsAttribute>().FirstOrDefault()
        ?? throw new Exception($"Symbols not found on subscription, try using the SymbolsAttirbute");

    private IDisposable Subscribe(Interval interval, IObserver<KlineEvent> observer, KlineSymbolsAttribute symbols)
    {
        var sendPreviousKlines = false;
        if (_symbols.TryGetValue((symbols.AggregatedSymbols, interval.GetDescription()), out var observers))
            sendPreviousKlines = observers.Add(observer);
        else
        {
            observers = new() { observer };
            _symbols.TryAdd((symbols.AggregatedSymbols, interval.GetDescription()), observers);
        }

        if (sendPreviousKlines)
            foreach (KlineEvent kline in _klines.Where(k => k.Symbol == symbols.AggregatedSymbols))
                observer.OnNext(kline);

        return new Unsubscriber<KlineEvent>(observers, observer);
    }

    public async Task<IDisposable> Subscribe(Interval interval, IObserver<KlineEvent> observer)
    {
        var symbols = GetSymbolsAttribute(observer);
        await AddSymbol(symbols, interval);
        return Subscribe(interval, observer, symbols);
    }

    public async Task<IEnumerable<IDisposable>> Subscribe(Interval interval, params IObserver<KlineEvent>[] observers) =>
        await Subscribe(interval, observers.ToList());

    public async Task<IEnumerable<IDisposable>> Subscribe(Interval interval, IEnumerable<IObserver<KlineEvent>> observers)
    {
        if (!observers.Any()) throw new Exception("You must provide observers for the subscription");

        var observersSymbols = observers.Select(o => new
        {
            Observer = o,
            Symbols = GetSymbolsAttribute(o)
        });
        await AddSymbols(observersSymbols.Select(o => o.Symbols), interval);
        return observersSymbols.Select(s => Subscribe(interval, s.Observer, s.Symbols));
    }
}
