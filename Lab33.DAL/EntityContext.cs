using Lab33.DAL.Entities;
using Lab33.DAL.Providers;

namespace Lab33.DAL;

// Контекст «бази даних» у файлі: відкриття/створення, оновлення (збереження і перечитування),
// закриття та видалення файлу. Формат файлу визначає провайдер.
public class EntityContext<T> where T : class, IEntity, new()
{
    private IDataProvider<T>? _provider;
    private string? _path;
    private List<T> _items = new();

    public bool IsOpen => _provider != null;

    public string? FilePath => _path;

    public IReadOnlyList<T> Items
    {
        get
        {
            EnsureOpen();
            return _items;
        }
    }

    // Відкриває файл (або створює порожню БД, якщо файлу ще немає).
    public void Open(string path, SerializationType type)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new DataAccessException("Не вказано ім'я файлу.");

        var provider = DataProviderFactory.Create<T>(type);
        if (string.IsNullOrEmpty(Path.GetExtension(path)))
            path += provider.Extension;

        var items = File.Exists(path) ? provider.Load(path) : new List<T>();

        _provider = provider;
        _path = path;
        _items = items;
    }

    public T? Find(string key)
    {
        EnsureOpen();
        return _items.FirstOrDefault(i => i.Key == key);
    }

    public void Add(T item)
    {
        EnsureOpen();
        _items.Add(item);
    }

    public bool Remove(string key)
    {
        EnsureOpen();
        return _items.RemoveAll(i => i.Key == key) > 0;
    }

    // Записує поточний стан у файл.
    public void Save()
    {
        EnsureOpen();
        _provider!.Save(_path!, _items);
    }

    // Перечитує дані з файлу, відкидаючи незбережені зміни.
    public void Reload()
    {
        EnsureOpen();
        _items = File.Exists(_path!) ? _provider!.Load(_path!) : new List<T>();
    }

    public void Close()
    {
        _provider = null;
        _path = null;
        _items = new List<T>();
    }

    public void DeleteFile()
    {
        EnsureOpen();
        try
        {
            if (File.Exists(_path!))
                File.Delete(_path!);
        }
        catch (Exception ex)
        {
            throw new DataAccessException($"Не вдалося видалити файл '{_path}': {ex.Message}", ex);
        }
        Close();
    }

    private void EnsureOpen()
    {
        if (_provider is null)
            throw new DataAccessException("Файл бази даних не відкрито.");
    }
}
