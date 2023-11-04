namespace CryptoBot.Infrastructure.Service.Observables;

internal sealed class Unsubscriber<T> : IDisposable
    where T : class
{
    private readonly ISet<IObserver<T>> _observers;
    private readonly IObserver<T> _observer;

    internal Unsubscriber(ISet<IObserver<T>> observers, IObserver<T> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose() => _observers.Remove(_observer);
}
