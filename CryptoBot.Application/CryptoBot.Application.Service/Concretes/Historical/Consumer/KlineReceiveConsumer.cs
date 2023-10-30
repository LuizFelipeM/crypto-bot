using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.CrossCutting.DTOs;

namespace CryptoBot.Application.Service.Historical.Consumer;

public class KlineReceiveConsumer : LavinMQReceiveConsumer<KlineContract>, ILavinMQReceiveConsumer<KlineContract>
{
    public override Task ProcessMessage(KlineContract payload)
    {
        // throw new NotImplementedException();
        return Task.CompletedTask;
    }
}