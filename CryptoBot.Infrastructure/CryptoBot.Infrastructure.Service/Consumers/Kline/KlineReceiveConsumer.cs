using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.Infrastructure.Service.Contracts;

namespace CryptoBot.Infrastructure.Service.Consumers.Kline;

public class KlineReceiveConsumer : LavinMQReceiveConsumer<IEnumerable<KlineContract>>, ILavinMQReceiveConsumer<IEnumerable<KlineContract>>
{
    public override async Task ProcessMessage(IEnumerable<KlineContract> payload)
    {
        throw new NotImplementedException();
    }
}