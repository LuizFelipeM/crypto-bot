using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.Infrastructure.Service.Contracts;

namespace CryptoBot.Infrastructure.Service.Concretes.Kline;

public class KlineReceiveConsumer : LavinMQReceiveConsumer<KlineContract>, ILavinMQReceiveConsumer<KlineContract>
{
    public override async Task ProcessMessage(KlineContract payload)
    {
        throw new NotImplementedException();
    }
}