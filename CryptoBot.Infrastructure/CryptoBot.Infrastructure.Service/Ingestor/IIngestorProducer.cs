using CryptoBot.Application.LavinMQ.Contract.Interfaces;

namespace CryptoBot.Infrastructure.Service.Ingestor;

public interface IIngestorProducer<T> : ILavinMQProducer<T>
    where T : class
{

}