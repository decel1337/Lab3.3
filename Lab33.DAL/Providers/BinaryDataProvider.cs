using System.Runtime.Serialization.Formatters.Binary;

namespace Lab33.DAL.Providers;

public class BinaryDataProvider<T> : FileDataProvider<T> where T : class, new()
{
    public override string Extension => ".bin";

    protected override List<T> Read(Stream stream) =>
        new BinaryFormatter().Deserialize(stream) as List<T>
        ?? throw new InvalidDataException("Файл має неочікуваний формат.");

    protected override void Write(Stream stream, List<T> items) =>
        new BinaryFormatter().Serialize(stream, items);
}
