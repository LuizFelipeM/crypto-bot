
namespace CryptoBot.Application.LavinMQ.Contract.Interfaces;

public interface ILavinMQReceiveConsumer<T>
    where T : class
{
    Task<MessageForConsumeRespnse<T>> Receive(List<MessageForConsume<T>> processes);
}