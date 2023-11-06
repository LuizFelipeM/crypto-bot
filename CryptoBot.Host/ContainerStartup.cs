using System.Configuration;
using System.Reflection;
using CryptoBot.Application.Binance.Client.API;
using CryptoBot.Application.Binance.Client.Historical;
using CryptoBot.Application.Binance.Client.Market;
using CryptoBot.Application.Binance.Contract;
using CryptoBot.Application.Binance.Contract.Interfaces;
using CryptoBot.Application.LavinMQ.Client;
using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Services.Observables;
using CryptoBot.Host.Configs.Entities;
using CryptoBot.Infrastructure.Job;
using CryptoBot.Infrastructure.Repository.MySql;
using CryptoBot.Infrastructure.Repository.MySql.Contexts;
using CryptoBot.Infrastructure.Service;
using CryptoBot.Infrastructure.Service.Auth;
using CryptoBot.Infrastructure.Service.Consumers.Kline;
using CryptoBot.Infrastructure.Service.Contracts;
using CryptoBot.Infrastructure.Service.Historical.Producer;
using CryptoBot.Infrastructure.Service.Ingestor;
using CryptoBot.Infrastructure.Service.Observables;
using CryptoBot.Infrastructure.Service.User;
using Microsoft.EntityFrameworkCore;
using Quartz;
using YamlDotNet.Serialization;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace CryptoBot.Host;

public static class ContainerStartup
{
    private static IEnumerable<Type> GetTypesWithCustomAttributes<TAttribute, TConcrete>()
        where TConcrete : class
        where TAttribute : Attribute
    {
        var assembly = typeof(TConcrete).Assembly;
        return GetTypesWithCustomAttributes<TAttribute>(assembly);
    }

    private static IEnumerable<Type> GetTypesWithCustomAttributes<TAttribute>(Assembly assembly)
        where TAttribute : Attribute
    {
        foreach (Type type in assembly.GetTypes())
            if (type.GetCustomAttributes(typeof(TAttribute), true).Length > 0)
                yield return type;
    }

    private static IEnumerable<Type> GetTypesInAssembly<TInterface, TConcrete>()
        where TConcrete : class, TInterface
    {
        var assembly = typeof(TConcrete).Assembly;
        return assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => i.Module == typeof(TInterface).Module));
    }

    public static T? ReadYamlFile<T>(string filePath)
    {
        if (!filePath.Contains(".yaml") && !filePath.Contains(".yml")) filePath += ".yaml";
        if (!File.Exists(filePath)) throw new FileNotFoundException($"File {filePath} not found");

        string yamlContent = File.ReadAllText(filePath);

        // Create a deserializer
        var deserializer = new DeserializerBuilder().Build();

        // Deserialize the YAML content into an object
        var yamlObject = deserializer.Deserialize<T>(yamlContent);

        // Now you can work with the deserialized object
        return yamlObject ?? throw new Exception("File is empty");
    }


    public static void RegisterServices(ConfigurationManager configuration, IServiceCollection services)
    {
        var binanceConfig = configuration.GetSection("Binance").Get<BinanceConfig>() ?? new();
        services.AddSingleton(binanceConfig)
                .AddSingleton<IBinanceSpotClient, BinanceSpotClient>()
                .AddSingleton<IBinanceHistoricalClient, BinanceHistoricalClient>();

        services.AddSingleton<IKlineClient, KlineClient>()
                .AddSingleton<IKlineObservable, KlineObservable>();

        // Services initialization
        services.AddScoped<IKlineService, KlineService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>();
    }

    public static void RegisterRepositories(ConfigurationManager configuration, IServiceCollection services)
    {
        services.AddDbContext<MySqlDbContext>(options =>
        {
            var mySqlConfig = configuration.GetSection("MySql").Get<MySqlConfig>();
            options.UseMySql(mySqlConfig.ConnectionString, ServerVersion.AutoDetect(mySqlConfig.ConnectionString));
        });

        services.AddScoped<IKlineRepository, KlineRepository>()
                .AddScoped<IUserRepository, UserRepository>();
    }

    public static void RegisterJobs(ConfigurationManager configuration, IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var types = GetTypesInAssembly<IJob, KlineJob>();
            var jobsConfig = ReadYamlFile<Dictionary<string, JobConfig>>("./Configs/Jobs/jobs.Development.yaml");
            types
                .Where(t => !t.IsAbstract)
                .ToList()
                .ForEach(jobImplementation =>
                {
                    if (!jobsConfig.TryGetValue(jobImplementation.Name, out var config))
                        throw new SettingsPropertyNotFoundException($"Job {jobImplementation.Name} configuration is missing");

                    if (!config.Active) return;

                    var jobKey = new JobKey(jobImplementation.Name);
                    q.AddJob(jobImplementation, jobKey, opts => opts.WithIdentity(jobKey));
                    q.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .WithIdentity($"{jobImplementation.Name}-trigger")
                        .WithCronSchedule(config.Cron));
                });
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    public static void RegisterLavinMQClients(ConfigurationManager configuration, IServiceCollection services)
    {
        var lavinMQHostConfig = configuration.GetSection("LavinMQ").Get<LavinMQHostConfig>() ?? throw new Exception("LavinMQ configuration not found");
        services.AddSingleton(lavinMQHostConfig);
        LavinMQStartup(lavinMQHostConfig);

        var ingestorProducerConfig = configuration.GetSection("LavinMQ:Producers:Ingestor").Get<IngestorProducerConfig>();
        services.AddSingleton(ingestorProducerConfig);
        services.AddSingleton(typeof(IIngestorProducer<>), typeof(IngestorProducer<>));

        var klineConsumerConfig = configuration.GetSection("LavinMQ:Consumers:Kline").Get<KlineConsumerConfig>();
        services.AddSingleton(klineConsumerConfig);
        services.AddScoped<ILavinMQConsumer<IEnumerable<KlineContract>>, KlineConsumer>();
        services.AddScoped<ILavinMQReceiveConsumer<IEnumerable<KlineContract>>, KlineReceiveConsumer>();
    }

    private static void LavinMQStartup(LavinMQHostConfig hostConfig)
    {
        var infrastructureConfigs = ReadYamlFile<LavinMQInfrastructureConfigs>("./Configs/LavinMQ/infrastructure.Development.yaml");
        var startup = new LavinMQStartup(hostConfig);

        foreach (var (name, config) in infrastructureConfigs.Exchanges)
        {
            if (string.IsNullOrEmpty(config.Name)) config.Name = name;
            startup.Create(config).Wait();
        }

        foreach (var (name, config) in infrastructureConfigs.Queues)
        {
            if (string.IsNullOrEmpty(config.Name)) config.Name = name;
            startup.Create(config).Wait();
        }

        startup.Dispose();
    }
}