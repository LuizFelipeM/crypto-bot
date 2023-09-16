using CryptoBot.Application.Binance.Client;
using CryptoBot.Application.Binance.Client.API;
using CryptoBot.Application.Binance.Client.Streams;
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
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton((_) =>
        {
            var connection = new SQLiteAsyncConnection(configuration.GetConnectionString("LiveMarket"));
            connection.CreateTableAsync<Order>().Wait();
            return connection;
        });
    }
}