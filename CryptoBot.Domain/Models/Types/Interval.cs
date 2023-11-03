using System.ComponentModel;

namespace CryptoBot.Domain.Models.Types;

public enum Interval
{
    [Description("1s")]
    ONE_SECOND,

    [Description("1m")]
    ONE_MINUTE,

    [Description("3m")]
    THREE_MINUTE,

    [Description("5m")]
    FIVE_MINUTE,

    [Description("15m")]
    FIFTEEN_MINUTE,

    [Description("30m")]
    THIRTY_MINUTE,

    [Description("1h")]
    ONE_HOUR,

    [Description("2h")]
    TWO_HOUR,

    [Description("4h")]
    FOUR_HOUR,

    [Description("6h")]
    SIX_HOUR,

    [Description("8h")]
    EIGTH_HOUR,

    [Description("12h")]
    TWELVE_HOUR,

    [Description("1d")]
    ONE_DAY,

    [Description("3d")]
    THREE_DAY,

    [Description("1w")]
    ONE_WEEK,

    [Description("1M")]
    ONE_MONTH,

}