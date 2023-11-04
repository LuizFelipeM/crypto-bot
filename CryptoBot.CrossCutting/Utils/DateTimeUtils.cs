namespace CryptoBot.CrossCutting;

public static class DateTimeUtils
{
    public static long ToUnixMilliseconds(this DateTime date) =>
        new DateTimeOffset(date).ToUnixTimeMilliseconds();

    public static DateTime FromUnixTimeMilliseconds(this long milliseconds) =>
        DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
}
