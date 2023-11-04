using CryptoBot.Domain.Models.Types;

namespace CryptoBot.Domain.Interfaces.Services.Observables;

public interface IKlineObservable
{
    Task<IDisposable> Subscribe(IObserver<KlineEvent> observer, Interval interval);
    Task<IEnumerable<IDisposable>> Subscribe(IEnumerable<IObserver<KlineEvent>> observers, Interval interval);
}
