using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Application.Service.Historical.Producer;

namespace CryptoBot.Application.Service.Ingestor;

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