using CryptoBot.Application.Binance.Contract;
using CryptoBot.Domain;

namespace CryptoBot.Infrastructure.Service.Observers;

[KlineSymbols(Symbol.BTC, Symbol.USDT)]
public class OneInchBtcObserver : IObserver<KlineEvent>
{
    public OneInchBtcObserver()
    {
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(KlineEvent kline)
    {
        throw new NotImplementedException();
    }
}
