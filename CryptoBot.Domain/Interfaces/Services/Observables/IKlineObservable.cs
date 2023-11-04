using CryptoBot.Domain.Models.Types;

namespace CryptoBot.Domain.Interfaces.Services.Observables;

public interface IKlineObservable
{
    Task<IDisposable> Subscribe(Interval interval, IObserver<KlineEvent> observer);
    Task<IEnumerable<IDisposable>> Subscribe(Interval interval, params IObserver<KlineEvent>[] observers);
    Task<IEnumerable<IDisposable>> Subscribe(Interval interval, IEnumerable<IObserver<KlineEvent>> observers);
}
