namespace Lab33.DAL.Providers;

public static class DataProviderFactory
{
    public static IDataProvider<T> Create<T>(SerializationType type) where T : class, new() => type switch
    {
        SerializationType.Binary => new BinaryDataProvider<T>(),
        SerializationType.Xml => new XmlDataProvider<T>(),
        SerializationType.Json => new JsonDataProvider<T>(),
        SerializationType.Custom => new CustomDataProvider<T>(),
        _ => throw new DataAccessException($"Непідтримуваний тип серіалізації: {type}.")
    };
}
