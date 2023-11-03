using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.Infrastructure.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoBot.Infrastructure.Service.Concretes.Kline;

public class KlineConsumer : LavinMQConsumer<IEnumerable<KlineContract>>, ILavinMQConsumer<IEnumerable<KlineContract>>
{
    public KlineConsumer(
        LavinMQHostConfig hostConfig,
        KlineConsumerConfig consumerConfig,
        ILavinMQReceiveConsumer<IEnumerable<KlineContract>> receiveConsumer,
        IServiceScopeFactory serviceScopeFactory
    ) : base(hostConfig, consumerConfig, receiveConsumer, serviceScopeFactory)
    {
    }
}