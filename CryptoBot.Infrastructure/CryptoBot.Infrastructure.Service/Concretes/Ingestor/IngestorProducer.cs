using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Infrastructure.Service.Historical.Producer;

namespace CryptoBot.Infrastructure.Service.Ingestor;

public class IngestorProducer<T> : LavinMQProducer<T>, IIngestorProducer<T>
    where T : class
{
    public IngestorProducer(
        LavinMQHostConfig hostConfig,
        IngestorProducerConfig producerConfig
    ) : base(hostConfig, producerConfig)
    {
    }
}