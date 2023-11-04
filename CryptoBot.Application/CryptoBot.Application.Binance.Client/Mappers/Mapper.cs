using CryptoBot.Domain.Models.Types;
using BinanceInterval = Binance.Spot.Models.Interval;

namespace CryptoBot.Application.Binance.Client.Mappers;

public static class Mapper
{
    public static BinanceInterval Map(Interval interval) =>
        interval switch
        {
            Interval.ONE_SECOND => BinanceInterval.ONE_SECOND,
            Interval.ONE_MINUTE => BinanceInterval.ONE_MINUTE,
            Interval.THREE_MINUTE => BinanceInterval.THREE_MINUTE,
            Interval.FIVE_MINUTE => BinanceInterval.FIVE_MINUTE,
            Interval.FIFTEEN_MINUTE => BinanceInterval.FIFTEEN_MINUTE,
            Interval.THIRTY_MINUTE => BinanceInterval.THIRTY_MINUTE,
            Interval.ONE_HOUR => BinanceInterval.ONE_HOUR,
            Interval.TWO_HOUR => BinanceInterval.TWO_HOUR,
            Interval.FOUR_HOUR => BinanceInterval.FOUR_HOUR,
            Interval.SIX_HOUR => BinanceInterval.SIX_HOUR,
            Interval.EIGTH_HOUR => BinanceInterval.EIGTH_HOUR,
            Interval.TWELVE_HOUR => BinanceInterval.TWELVE_HOUR,
            Interval.ONE_DAY => BinanceInterval.ONE_DAY,
            Interval.THREE_DAY => BinanceInterval.THREE_DAY,
            Interval.ONE_WEEK => BinanceInterval.ONE_WEEK,
            Interval.ONE_MONTH => BinanceInterval.ONE_MONTH,
            _ => throw new NotImplementedException(),
        };
}