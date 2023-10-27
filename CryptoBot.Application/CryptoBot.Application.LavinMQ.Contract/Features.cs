namespace CryptoBot.Application.LavinMQ.Contract;

public struct Features
{
    public static Features DEAD_LETTER_EXCHANGE => new("x-dead-letter-exchange");
    public static Features DEAD_LETTER_ROUTING_KEY => new("x-dead-letter-routing-key");

    public string Value { get; private set; }

    public Features(string value) => Value = value;

    public static implicit operator string(Features enm) => enm.Value;
    public override readonly string ToString() => Value.ToString();
}