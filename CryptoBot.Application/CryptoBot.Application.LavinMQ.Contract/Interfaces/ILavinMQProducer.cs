namespace CryptoBot.Application.LavinMQ.Contract.Interfaces;

public interface ILavinMQProducer<T> : IDisposable
    where T : class
{
    Task Publish(T message, string? routingKey = null);
}