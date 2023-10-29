using CryptoBot.Application.LavinMQ.Contract.Configs;
using CryptoBot.Application.LavinMQ.Contract.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CryptoBot.Application.LavinMQ.Client;

public class LavinMQClient : ILavinMQClient, IDisposable
{
    protected readonly ConnectionFactory _factory;
    protected IConnection? _connection;
    protected bool _errorConnection;
    protected IModel? _channel;
    protected readonly LavinMQHostConfig _config;

    public LavinMQClient(LavinMQHostConfig config)
    {
        _errorConnection = false;
        _config = config;
        _factory = new ConnectionFactory
        {
            UserName = config.UserName,
            Password = config.Password,
            Port = config.Port,
            DispatchConsumersAsync = config.DispatchConsumersAsync,
            VirtualHost = config.VirtualHost,
            RequestedHeartbeat = config.RequestedHeartbeat,
            AutomaticRecoveryEnabled = config.AutomaticRecoveryEnabled,
            HostName = config.HostName,
            Ssl = new SslOption()
            {
                ServerName = config.HostName,
                Enabled = config.SslEnabled
            }
        };
    }

    private void ConnectionShutdown(object connection, ShutdownEventArgs args) => _errorConnection = true;
    private void ConnectionBlocked(object connection, ConnectionBlockedEventArgs args) => _errorConnection = true;
    private void CallbackException(object connection, CallbackExceptionEventArgs args) => _errorConnection = true;

    public async Task Connect()
    {
        _connection = _factory.CreateConnection();
        _connection.ConnectionShutdown += ConnectionShutdown;
        _connection.ConnectionBlocked += ConnectionBlocked;
        _connection.CallbackException += CallbackException;
        _channel = _connection.CreateModel();
    }

    public virtual void Dispose()
    {
        if (_connection != null && _connection.IsOpen) _connection.Close();
        _connection?.Dispose();
    }
}