using CryptoBot.Domain.Models.Types;

namespace CryptoBot.Domain;

public delegate void KlineReceived(KlineEvent kline);
public delegate void KlineErrorReceived(Exception exception);
public delegate void Disconnection();

public interface IKlineClient
{
    event KlineReceived? OnKlineReceived;
    event KlineErrorReceived? OnKlineErrorReceived;
    event Disconnection? OnDisconnection;

    Task Watch(string symbol, Interval interval);
    Task Watch(string[] symbols, Interval interval);
}
