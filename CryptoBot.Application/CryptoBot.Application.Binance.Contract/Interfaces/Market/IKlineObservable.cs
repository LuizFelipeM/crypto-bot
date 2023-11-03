using Binance.Spot.Models;

namespace CryptoBot.Application.Binance.Contract;

public interface IKlineObservable
{
    Task<IDisposable> Subscribe(IObserver<Kline> observer, Interval interval);
    Task<IEnumerable<IDisposable>> Subscribe(IEnumerable<IObserver<Kline>> observers, Interval interval);
}
