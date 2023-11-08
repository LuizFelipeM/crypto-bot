using System.ComponentModel;
using System.Reflection;

namespace CryptoBot.CrossCutting.Utils;

public static class AttributeUtils
{
    public static IEnumerable<T?> GetCustomAttributes<T>(this object obj) where T : Attribute =>
        Attribute.GetCustomAttributes(obj.GetType()).Where(a => a is T).Select(a => (T)a ?? default);

    public static string GetDescription<T>(this T value) where T : Enum
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            return attributes.First().Description;
        return value.ToString();
    }
}
