using System.Text.Json;

namespace Lab33.DAL.Providers;

public class JsonDataProvider<T> : FileDataProvider<T> where T : class, new()
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public override string Extension => ".json";

    protected override List<T> Read(Stream stream) =>
        JsonSerializer.Deserialize<List<T>>(stream, Options)
        ?? throw new InvalidDataException("Файл має неочікуваний формат.");

    protected override void Write(Stream stream, List<T> items) =>
        JsonSerializer.Serialize(stream, items, Options);
}
