using CryptoBot.Application.LavinMQ.Contract;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;

namespace CryptoBot.Application.LavinMQ.Client;

public abstract class LavinMQReceiveConsumer<T> : ILavinMQReceiveConsumer<T>
    where T : class
{
    public abstract Task ProcessMessage(T payload);

    public virtual async Task<MessageForConsumeRespnse<T>> Receive(List<MessageForConsume<T>> processes)
    {
        MessageForConsumeRespnse<T> resp = new();
        foreach (var process in processes)
        {
            try
            {
                await ProcessMessage(process.Entity);
                resp.SuccessMessages.Add(process.DeliveryTag, process);
            }
            catch (Exception)
            {
                resp.ErrorMessages.Add(process.DeliveryTag, process);
            }
        }
        return resp;
    }
}