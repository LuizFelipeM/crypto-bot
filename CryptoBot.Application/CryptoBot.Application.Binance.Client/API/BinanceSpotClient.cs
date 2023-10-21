using Binance.Common;
using Binance.Spot;
using CryptoBot.Application.Binance.Contract;
using CryptoBot.Application.Binance.Contract.DTOs;
using CryptoBot.Application.Binance.Contract.Interfaces;
using Microsoft.Extensions.Logging;

namespace CryptoBot.Application.Binance.Client.API;

public class BinanceSpotClient : IBinanceSpotClient
{
    private readonly ILogger<BinanceSpotClient> _logger;
    private readonly BinanceConfig _config;
    private readonly SpotAccountTrade _client;

    public BinanceSpotClient(ILogger<BinanceSpotClient> logger, BinanceConfig config)
    {
        _logger = logger;
        _config = config;
        _client = new SpotAccountTrade(
            new HttpClient(new BinanceLoggingHandler(_logger)),
            baseUrl: _config.BaseUrl,
            apiKey: _config.ApiKey,
            apiSecret: _config.ApiSecret);
    }

    public async Task<string> NewOrder(NewOrderDto newOrder)
    {
        return await _client.NewOrder(
            symbol: newOrder.Symbol,
            side: newOrder.Side,
            type: newOrder.Type,
            timeInForce: newOrder.TimeInForce,
            quantity: newOrder.Quantity,
            quoteOrderQty: newOrder.QuoteOrderQty,
            price: newOrder.Price,
            newClientOrderId: newOrder.NewClientOrderId,
            strategyId: newOrder.StrategyId,
            strategyType: newOrder.StrategyType,
            stopPrice: newOrder.StopPrice,
            trailingDelta: newOrder.TrailingDelta,
            icebergQty: newOrder.IcebergQty,
            newOrderRespType: newOrder.NewOrderRespType,
            recvWindow: newOrder.RecvWindow
        );
    }
}