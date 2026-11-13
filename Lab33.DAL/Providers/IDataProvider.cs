namespace Lab33.DAL.Providers;

// Абстракція провайдера даних: кожен тип серіалізації має свою реалізацію.
public interface IDataProvider<T> where T : class, new()
{
    // Розширення файлу за замовчуванням (з крапкою).
    string Extension { get; }

    List<T> Load(string path);

    void Save(string path, IEnumerable<T> items);
}
