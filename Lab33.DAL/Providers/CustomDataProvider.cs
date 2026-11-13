using System.Globalization;
using System.Reflection;
using System.Text;

namespace Lab33.DAL.Providers;

// Користувацька серіалізація: власний текстовий формат.
// Кожен об'єкт — блок "[Тип]" з рядками "Властивість=Значення" (значення кодуються
// через Uri.EscapeDataString), блоки розділені порожнім рядком.
// Працює для будь-якого класу з публічними властивостями get/set.
public class CustomDataProvider<T> : FileDataProvider<T> where T : class, new()
{
    private static readonly PropertyInfo[] Properties = typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0)
        .ToArray();

    public override string Extension => ".txt";

    protected override void Write(Stream stream, List<T> items)
    {
        using var writer = new StreamWriter(stream, new UTF8Encoding(false));
        foreach (var item in items)
        {
            writer.WriteLine($"[{typeof(T).Name}]");
            foreach (var property in Properties)
                writer.WriteLine($"{property.Name}={Uri.EscapeDataString(ToText(property.GetValue(item)))}");
            writer.WriteLine();
        }
    }

    protected override List<T> Read(Stream stream)
    {
        var byName = Properties.ToDictionary(p => p.Name);
        var result = new List<T>();
        T? current = null;

        using var reader = new StreamReader(stream, Encoding.UTF8);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Length == 0)
                continue;

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                current = new T();
                result.Add(current);
                continue;
            }

            int separator = line.IndexOf('=');
            if (current is null || separator < 0 || !byName.TryGetValue(line[..separator], out var property))
                throw new FormatException($"Некоректний рядок: '{line}'");

            var text = Uri.UnescapeDataString(line[(separator + 1)..]);
            property.SetValue(current, FromText(text, property.PropertyType));
        }

        return result;
    }

    private static string ToText(object? value) => value switch
    {
        null => "",
        DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture),
        _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? ""
    };

    private static object FromText(string text, Type type) =>
        type == typeof(DateTime)
            ? DateTime.Parse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            : Convert.ChangeType(text, type, CultureInfo.InvariantCulture);
}
