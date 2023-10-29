using CryptoBot.CrossCutting.DTOs;
using Nelibur.ObjectMapper;

namespace CryptoBot.Application.Service.Mappers;

public static class MapperStartup
{
    public static void RegisterMappers()
    {
        TinyMapper.Bind<KlineDto, KlineContract>();
    }
}