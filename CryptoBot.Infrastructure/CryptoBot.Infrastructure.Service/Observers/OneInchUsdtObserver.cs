using CryptoBot.Application.Binance.Contract;
using CryptoBot.Domain;

namespace CryptoBot.Infrastructure.Service.Observers;

[KlineSymbols(Symbol.ONE_INCH, Symbol.USDT)]
public class OneInchUsdtObserver : IObserver<Kline>
{
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Kline value)
    {
        throw new NotImplementedException();
    }
}
