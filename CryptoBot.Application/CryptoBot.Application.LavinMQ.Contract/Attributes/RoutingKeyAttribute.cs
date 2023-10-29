namespace CryptoBot.Application.LavinMQ.Contract.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RoutingKeyAttribute : Attribute
{
    public string RoutingKey;

    public RoutingKeyAttribute(string routingKey)
    {
        RoutingKey = routingKey;
    }
}