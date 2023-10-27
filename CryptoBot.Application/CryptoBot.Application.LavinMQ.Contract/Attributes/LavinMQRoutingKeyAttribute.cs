namespace CryptoBot.Application.LavinMQ.Contract.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class LavinMQRoutingKeyAttribute : Attribute
{
    public string RoutingKey;
    public LavinMQRoutingKeyAttribute(string routingKey)
    {
        RoutingKey = routingKey;
    }
}