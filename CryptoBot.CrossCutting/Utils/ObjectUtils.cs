using System.Reflection;

namespace CryptoBot.CrossCutting.Utils;

public static class ObjectUtils
{
    public static Dictionary<string, T> GetFieldValues<T>(Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(T))
            .ToDictionary(f => f.Name, f => (T)f.GetValue(null));


}
