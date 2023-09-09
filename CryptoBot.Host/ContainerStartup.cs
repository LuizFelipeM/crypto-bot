using CryptoBot.Application.Binance.Client;

namespace CryptoBot.Host;

public static class ContainerStartup
{
    public static void RegisterServices(ConfigurationManager configuration, IServiceCollection services)
    {
        services.AddSingleton(configuration.GetSection("Binance").Get<BinanceConfig>() ?? new());
        services.AddSingleton<IBinanceClient, BinanceClient>();
    }
}