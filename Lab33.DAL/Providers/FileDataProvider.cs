namespace Lab33.DAL.Providers;

// Базовий провайдер: уся робота з файлом і обробка помилок тут,
// похідні класи реалізують лише читання/запис потоку у своєму форматі.
public abstract class FileDataProvider<T> : IDataProvider<T> where T : class, new()
{
    public abstract string Extension { get; }

    public List<T> Load(string path)
    {
        try
        {
            if (new FileInfo(path).Length == 0)
                return new List<T>();

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return Read(stream);
        }
        catch (Exception ex)
        {
            throw new DataAccessException($"Не вдалося прочитати файл '{path}': {ex.Message}", ex);
        }
    }

    public void Save(string path, IEnumerable<T> items)
    {
        try
        {
            var directory = Path.GetDirectoryName(Path.GetFullPath(path));
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            Write(stream, items.ToList());
        }
        catch (Exception ex)
        {
            throw new DataAccessException($"Не вдалося записати файл '{path}': {ex.Message}", ex);
        }
    }

    protected abstract List<T> Read(Stream stream);

    protected abstract void Write(Stream stream, List<T> items);
}
