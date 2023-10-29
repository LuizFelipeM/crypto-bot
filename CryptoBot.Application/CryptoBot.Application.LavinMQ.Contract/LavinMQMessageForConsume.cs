using System.Text;
using System.Text.Json;
using RabbitMQ.Client.Events;

namespace CryptoBot.Application.LavinMQ.Contract;

public class LavinMQMessageForConsume<T> where T : class
{
    public T Entity { get; set; }
    public ulong DeliveryTag { get; set; }

    public static implicit operator LavinMQMessageForConsume<T>(BasicDeliverEventArgs basicDeliveryEventArgs)
    {
        var body = basicDeliveryEventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var entity = (typeof(T) == typeof(string)) ? message as T : JsonSerializer.Deserialize<T>(message);

        return new LavinMQMessageForConsume<T>
        {
            Entity = entity,
            DeliveryTag = basicDeliveryEventArgs.DeliveryTag,
        };
    }
}