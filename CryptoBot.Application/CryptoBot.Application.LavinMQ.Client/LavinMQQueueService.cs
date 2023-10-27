using CryptoBot.Application.LavinMQ.Contract;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using RabbitMQ.Client;

namespace CryptoBot.Application.LavinMQ.Client;

public class LavinMQQueueService
{
    private readonly LavinMQQueueConfig _queueConfig;
    private readonly IModel _channel;

    public LavinMQQueueService(LavinMQQueueConfig queueConfig, IModel channel)
    {
        _queueConfig = queueConfig;
        _channel = channel;
    }

    public void Create()
    {
        CreateQueue(_queueConfig);
        if (_queueConfig.DeadLetterQueue != null)
        {
            CreateQueue(_queueConfig.DeadLetterQueue);
        }
    }

    private void CreateQueue(LavinMQQueueConfig queueConfig)
    {
        DeleteQueue(queueConfig);
        var arguments = new Dictionary<string, object>();
        if (queueConfig.Features != null)
        {
            if (queueConfig.Features.DeadLetterExchange != null)
                arguments.Add(Features.DEAD_LETTER_EXCHANGE, queueConfig.Features.DeadLetterExchange);

            if (queueConfig.Features.DeadLetterRoutingKey != null)
                arguments.Add(Features.DEAD_LETTER_ROUTING_KEY, queueConfig.Features.DeadLetterRoutingKey);
        }
        _channel.QueueDeclare(queue: queueConfig.Name,
                            durable: queueConfig.Durable,
                            exclusive: queueConfig.Exclusive,
                            autoDelete: queueConfig.AutoDelete,
                            arguments: arguments);

        foreach (var bind in queueConfig.BindArguments)
        {
            _channel.QueueBind(queue: queueConfig.Name, exchange: bind.ExchangeName, bind.RoutingKey);
        }
    }

    private void DeleteQueue(LavinMQQueueConfig queueConfig)
    {
        throw new NotImplementedException();
    }
}