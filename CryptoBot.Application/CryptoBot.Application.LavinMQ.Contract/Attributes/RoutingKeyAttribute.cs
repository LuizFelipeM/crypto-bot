namespace CryptoBot.Application.LavinMQ.Contract.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RoutingKeyAttribute : Attribute
{
    public string RoutingKey { get; private set; }

    public RoutingKeyAttribute(string routingKey)
    {
        RoutingKey = routingKey;
    }
}