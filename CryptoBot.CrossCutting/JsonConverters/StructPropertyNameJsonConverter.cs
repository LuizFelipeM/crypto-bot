using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoBot.CrossCutting.JsonConverters;

public class StructPropertyNameJsonConverter<T> : JsonConverter<T> where T : struct
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var inputText = reader.GetString()!;
        var prop = typeToConvert.GetProperties().Where(p => p.Name == inputText).First();
        return (T)prop.GetValue(typeToConvert);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}