using Newtonsoft.Json;

namespace CryptoBot.CrossCutting;

public class MillisecondsDateTimeJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(DateTime);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        long milliseconds;

        try
        {
            milliseconds = long.Parse((string)reader.Value);
        }
        catch
        {
            milliseconds = (long)reader.Value;
        }

        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
