using CryptoBot.Domain;
using CryptoBot.CrossCutting.Utils;

namespace CryptoBot.Application.Binance.Contract;

[AttributeUsage(AttributeTargets.Class,
                AllowMultiple = true)]
public class KlineSymbolsAttribute : Attribute
{
    public Symbol BaseSymbol { get; private set; }
    public Symbol QuoteSymbol { get; private set; }
    public string AggregatedSymbols => BaseSymbol.GetDescription() + QuoteSymbol.GetDescription();

    public KlineSymbolsAttribute(Symbol baseSymbol, Symbol quoteSymbol)
    {
        BaseSymbol = baseSymbol;
        QuoteSymbol = quoteSymbol;
    }
}
