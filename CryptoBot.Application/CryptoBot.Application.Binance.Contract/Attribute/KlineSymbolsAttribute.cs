namespace CryptoBot.Application.Binance.Contract;

[AttributeUsage(AttributeTargets.Class)]
public class KlineSymbolsAttribute : Attribute
{
    public string BaseSymbol { get; private set; }
    public string QuoteSymbol { get; private set; }
    public string Symbol => BaseSymbol + QuoteSymbol;

    public KlineSymbolsAttribute(string baseSymbol, string quoteSymbol)
    {
        BaseSymbol = baseSymbol;
        QuoteSymbol = quoteSymbol;
    }
}
