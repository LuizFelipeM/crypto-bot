using CryptoBot.Application.Binance.Contract;
using CryptoBot.CrossCutting.DTOs;
using CryptoBot.Domain;
using CryptoBot.Domain.Models.Entities;
using CryptoBot.Infrastructure.Service.Contracts;
using Nelibur.ObjectMapper;

namespace CryptoBot.Host.Mappers;

public static class MapperStartup
{
    public static void RegisterMappers()
    {
        TinyMapper.Bind<KlineDto, KlineContract>();
        TinyMapper.Bind<Kline, KlineEvent>();
        TinyMapper.Bind<KlineEvent, KlineEntity>();
    }
}