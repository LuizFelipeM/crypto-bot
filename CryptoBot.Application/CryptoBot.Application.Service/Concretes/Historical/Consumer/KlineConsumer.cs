using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.CrossCutting.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoBot.Application.Service.Historical.Consumer;

public class KlineConsumer : LavinMQConsumer<KlineDto>, ILavinMQConsumer<KlineDto>
{
    public KlineConsumer(
        LavinMQHostConfig hostConfig,
        KlineConsumerConfig consumerConfig,
        ILavinMQReceiveConsumer<KlineDto> receiveConsumer,
        IServiceScopeFactory serviceScopeFactory
    ) : base(hostConfig, consumerConfig, receiveConsumer, serviceScopeFactory)
    {
    }
}