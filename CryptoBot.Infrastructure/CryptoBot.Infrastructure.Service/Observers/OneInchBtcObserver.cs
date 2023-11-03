using CryptoBot.Application.Binance.Contract;
using CryptoBot.Domain;

namespace CryptoBot.Infrastructure.Service.Observers;

[KlineSymbols(Symbol.BTC, Symbol.USDT)]
public class OneInchBtcObserver : IObserver<Kline>
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

    public void OnNext(Kline kline)
    {
        throw new NotImplementedException();
    }
}
