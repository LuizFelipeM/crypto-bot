using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace CryptoBot.Application.LavinMQ.Contract;

public class MessageForConsume<T> where T : class
{
    public T Entity { get; set; }
    public ulong DeliveryTag { get; set; }

    public static implicit operator MessageForConsume<T>(BasicDeliverEventArgs basicDeliveryEventArgs)
    {
        var body = basicDeliveryEventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var entity = (typeof(T) == typeof(string)) ? message as T : JsonConvert.DeserializeObject<T>(message);

        return new MessageForConsume<T>
        {
            Entity = entity,
            DeliveryTag = basicDeliveryEventArgs.DeliveryTag,
        };
    }
}