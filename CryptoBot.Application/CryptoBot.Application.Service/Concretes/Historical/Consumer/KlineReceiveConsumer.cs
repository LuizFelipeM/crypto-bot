using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.CrossCutting.DTOs;

namespace CryptoBot.Application.Service.Historical.Consumer;

public class KlineReceiveConsumer : LavinMQReceiveConsumer<KlineDto>, ILavinMQReceiveConsumer<KlineDto>
{
    public override Task ProcessMessage(KlineDto payload)
    {
        throw new NotImplementedException();
    }
}