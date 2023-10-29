namespace CryptoBot.Application.LavinMQ.Contract.Interfaces;

public interface ILavinMQProducer<T>
{
    Task Publish(T message, string? routingKey = null);
}