using Binance.Spot.Models;

namespace CryptoBot.Application.Binance.Contract.Interfaces.Market;

internal interface IKlineClient
{
    delegate void KlineReceived(Kline kline);
    event KlineReceived OnKlineReceived;

    delegate void KlineErrorReceived(Exception exception);
    event KlineErrorReceived OnKlineErrorReceived;

    delegate void Disconnection();
    event Disconnection OnDisconnection;

    Task Watch(string symbol, Interval interval);
    Task Watch(string[] symbols, Interval interval);
}