using System.Reflection;
using CryptoBot.Application.Binance.Client.API;
using CryptoBot.Application.Binance.Client.Streams;
using CryptoBot.Application.Binance.Contract;
using CryptoBot.Application.Binance.Contract.Interfaces;
using CryptoBot.Domain.Interfaces.Repositories;
using CryptoBot.Domain.Models;
using CryptoBot.Infrastructure.Repositories;
using SQLite;

namespace CryptoBot.Host;

public static class ContainerStartup
{
    public static void RegisterServices(ConfigurationManager configuration, IServiceCollection services)
    {
        var binanceConfig = configuration.GetSection("Binance").Get<BinanceConfig>() ?? new();
        binanceConfig.BaseUrl = configuration["Binance:BaseUrl"];
        services.AddSingleton(binanceConfig);
        services.AddSingleton<IBinanceMarketClient, BinanceMarketClient>();
        services.AddSingleton<IBinanceSpotClient, BinanceSpotClient>();
    }

    public static void RegisterRepositories(ConfigurationManager configuration, IServiceCollection services)
    {
        var connection = new SQLiteAsyncConnection(configuration.GetConnectionString("LiveMarket"));
        CreateTablesInAssembly<Order>(connection);

        services.AddSingleton(connection);
        services.AddTransient<IOrderRepository, OrderRepository>();
    }

    private static void CreateTablesInAssembly<T>(SQLiteAsyncConnection connection)
    {
        var assembly = typeof(T).Assembly;
        var types = GetTypesWithHelpAttribute<TableAttribute>(assembly);
        connection.CreateTablesAsync(types: types.ToArray()).Wait();
    }

    private static IEnumerable<Type> GetTypesWithHelpAttribute<T>(Assembly assembly)
    {
        foreach (Type type in assembly.GetTypes())
            if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                yield return type;
    }
}