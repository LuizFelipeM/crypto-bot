using System.ComponentModel;

namespace CryptoBot.Domain;

public enum Symbol
{
    [Description("USDT")]
    USDT,

    [Description("BTC")]
    BTC,

    [Description("1INCH")]
    ONE_INCH,
}
