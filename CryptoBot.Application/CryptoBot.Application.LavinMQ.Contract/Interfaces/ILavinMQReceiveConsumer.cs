
namespace CryptoBot.Application.LavinMQ.Contract.Interfaces;

public interface ILavinMQReceiveConsumer<T>
    where T : class
{
    Task<LavinMQMessageForConsumeResponse<T>> Receive(List<LavinMQMessageForConsume<T>> processes);
}