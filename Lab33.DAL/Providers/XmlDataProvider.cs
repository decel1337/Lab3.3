using System.Xml.Serialization;

namespace Lab33.DAL.Providers;

public class XmlDataProvider<T> : FileDataProvider<T> where T : class, new()
{
    private static readonly XmlSerializer Serializer = new(typeof(List<T>));

    public override string Extension => ".xml";

    protected override List<T> Read(Stream stream) =>
        Serializer.Deserialize(stream) as List<T>
        ?? throw new InvalidDataException("Файл має неочікуваний формат.");

    protected override void Write(Stream stream, List<T> items) =>
        Serializer.Serialize(stream, items);
}
