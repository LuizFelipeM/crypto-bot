namespace CryptoBot.Application.LavinMQ.Contract.Interfaces;

public interface ILavinMQClient : IDisposable
{
    Task Connect();
}