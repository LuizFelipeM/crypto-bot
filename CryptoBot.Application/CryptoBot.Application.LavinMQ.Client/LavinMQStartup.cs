using CryptoBot.Application.LavinMQ.Contract;
using CryptoBot.Application.LavinMQ.Contract.Configs;

namespace CryptoBot.Application.LavinMQ.Client;

public class LavinMQStartup : LavinMQClient
{
    public LavinMQStartup(LavinMQHostConfig config) : base(config)
    {
    }

    private async Task InitializeChannel()
    {
        if (_connection == null || !_connection.IsOpen)
            await Connect();
    }

    private void CreateQueue(LavinMQQueueConfig config)
    {
        _channel?.QueueDelete(config.Name, !config.ForceDelete, !config.ForceDelete);

        var arguments = new Dictionary<string, object>();
        if (config.Features != null)
        {
            if (config.Features.DeadLetterExchange != null)
                arguments.Add(Features.DEAD_LETTER_EXCHANGE, config.Features.DeadLetterExchange);

            if (config.Features.DeadLetterRoutingKey != null)
                arguments.Add(Features.DEAD_LETTER_ROUTING_KEY, config.Features.DeadLetterRoutingKey);
        }

        _channel?.QueueDeclare(queue: config.Name,
                               durable: config.Durable,
                               exclusive: config.Exclusive,
                               autoDelete: config.AutoDelete,
                               arguments: arguments);

        foreach (var bind in config.Bindings)
        {
            _channel?.QueueBind(queue: config.Name,
                                exchange: bind.ExchangeName,
                                routingKey: bind.RoutingKey,
                                arguments: null);
        }
    }

    public async Task Create(LavinMQQueueConfig config)
    {
        try
        {
            await InitializeChannel();
            CreateQueue(config);
            if (config.DeadLetterQueue != null) CreateQueue(config.DeadLetterQueue);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not empty")) await Connect();
            else throw;
        }
    }

    public async Task Create(LavinMQExchangeConfig config)
    {
        try
        {
            await InitializeChannel();
            _channel?.ExchangeDelete(config.Name, !config.ForceDelete);
            _channel?.ExchangeDeclare(exchange: config.Name,
                                      type: config.Type,
                                      durable: config.Durable,
                                      autoDelete: config.AutoDelete,
                                      arguments: config.Arguments);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not empty")) await Connect();
            else throw;
        }
    }
}