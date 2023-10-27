using System.Text;
using System.Text.Json;
using CryptoBot.Application.LavinMQ.Contract.Attributes;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;

namespace CryptoBot.Application.LavinMQ.Client;

public class LavinMQProducer<T> : LavinMQClient, ILavinMQProducer<T>
{
    private readonly LavinMQProducerConfig _producerConfig;

    public LavinMQProducer(LavinMQHostConfig hostConfig, LavinMQProducerConfig producerConfig) : base(hostConfig)
    {
        _producerConfig = producerConfig;
    }

    private async Task InitializeChannel()
    {
        if (_connection == null || !_connection.IsOpen)
            await Connect();
    }

    public async Task Publish(T message, string? routingKey = null)
    {
        await InitializeChannel();

        if (routingKey == null)
        {
            var routingKeyAttr = Attribute.GetCustomAttributes(typeof(T)).FirstOrDefault(a => a is LavinMQRoutingKeyAttribute);
            routingKey = routingKeyAttr != null ? ((LavinMQRoutingKeyAttribute)routingKeyAttr).RoutingKey : null;
        }

        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        _channel?.BasicPublish(exchange: _producerConfig.ExchangeName,
                              routingKey: routingKey,
                              mandatory: false,
                              basicProperties: null,
                              body: body);
    }
}
