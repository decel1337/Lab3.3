using Lab33.BLL.Models;

namespace Lab33.BLL.Services;

// Контракт сервісу, яким користується рівень представлення.
public interface IEntityService<TBll> where TBll : BllPerson
{
    bool IsOpen { get; }
    string? FilePath { get; }

    void Open(string path, StorageFormat format);
    IReadOnlyList<TBll> GetAll();
    void Add(TBll item);
    void Remove(string key);
    TBll Find(string key);
    string Sing(string key);
    void Save();
    void Close();
    void DeleteFile();
}
