using RabbitMQ.Client.Events;

namespace CryptoBot.Application.LavinMQ.Contract.Interfaces;

public interface ILavinMQConsumer<T> : IDisposable
    where T : class
{
    Task Consume();
    Task ProcessMessage(object ch, BasicDeliverEventArgs ea);
    Task InvokeConsume();
    bool HasConsumer();
}