using CryptoBot.Application.Binance.Contract;
using CryptoBot.Domain;

namespace CryptoBot.Infrastructure.Service.Observers;

[KlineSymbols(Symbol.ONE_INCH, Symbol.USDT)]
public class OneInchUsdtObserver : IObserver<KlineEvent>
{
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(KlineEvent value)
    {
        throw new NotImplementedException();
    }
}
