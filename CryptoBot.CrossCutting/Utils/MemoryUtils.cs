using CryptoBot.CrossCutting.Enums;
using Newtonsoft.Json;

namespace CryptoBot.CrossCutting.Utils;

public static class MemoryUtils
{
    /// <summary>
    /// Calculate the memory size of a object list
    /// </summary>
    /// <typeparam name="T">Object type for complex object types</typeparam>
    /// <param name="list">Object list to be calcualted</param>
    /// <returns>List size in bytes</returns>
    public static long GetSize<T>(this IEnumerable<T> list) =>
        list.Aggregate((long)0, (prev, curr) => prev + curr.GetSize());

    /// <summary>
    /// Calculate the memory size of a object
    /// </summary>
    /// <typeparam name="T">Object type for complex object types</typeparam>
    /// <param name="obj">Object to be calcualted</param>
    /// <returns>Object size in bytes</returns>
    public static long GetSize<T>(this T obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        return System.Text.Encoding.Unicode.GetByteCount(json);
    }

    /// <summary>
    /// Byte to unit conversor
    /// </summary>
    /// <param name="value">Byte value</param>
    /// <param name="unit">Desired unit for conversion</param>
    /// <returns>Equivalent size of bytes in the choosen measurement unit</returns>
    public static double ToSize(this long value, Unit unit) =>
        value / Math.Pow(1024, (int)unit);

    /// <summary>
    /// Unit to byte conversor
    /// </summary>
    /// <param name="value">Value in the choosen measurement unit</param>
    /// <param name="unit">Unit of the value to be converted</param>
    /// <returns>Equivalent byte size of the choosen measurement unit</returns>
    public static long FromSize(this double value, Unit unit) =>
        Convert.ToInt64(value * Math.Pow(1024, (int)unit));

    /// <summary>
    /// Split a list in multiple lists of the choosen memory size
    /// </summary>
    /// <typeparam name="T">List type</typeparam>
    /// <param name="list">Source list</param>
    /// <param name="size">Size value to split in the choosen unit</param>
    /// <param name="unit">Unit of the size value</param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, double size, Unit unit)
    {
        List<List<T>> result = new();
        long sizeValue = size.FromSize(unit);

        List<T> currentList = new();
        long currentListMemorySize = 0;
        foreach (var item in list)
        {
            long itemSize = item.GetSize();

            if (currentListMemorySize + itemSize <= sizeValue)
            {
                currentList.Add(item);
                currentListMemorySize += itemSize;
            }
            else
            {
                result.Add(currentList);
                currentListMemorySize = 0;
                currentList = new();
            }
        }

        return result.Append(currentList);
    }
}