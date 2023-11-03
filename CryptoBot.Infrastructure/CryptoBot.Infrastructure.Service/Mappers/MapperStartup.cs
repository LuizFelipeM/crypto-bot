using CryptoBot.CrossCutting.DTOs;
using CryptoBot.Infrastructure.Service.Contracts;
using Nelibur.ObjectMapper;

namespace CryptoBot.Infrastructure.Service.Mappers;

public static class MapperStartup
{
    public static void RegisterMappers()
    {
        TinyMapper.Bind<KlineDto, KlineContract>();
        TinyMapper.Bind<Domain.Models.Types.Interval, Binance.Spot.Models.Interval>();
    }
}