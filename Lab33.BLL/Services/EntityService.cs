using Lab33.BLL.Exceptions;
using Lab33.BLL.Models;
using Lab33.DAL;
using Lab33.DAL.Entities;

namespace Lab33.BLL.Services;

// Загальна логіка сервісу: додавання, видалення, пошук. Працює лише через EntityContext
// (без Console і Stream). Помилки DAL перетворюються на винятки BLL, але не обробляються —
// їх обробляє рівень представлення.
public abstract class EntityService<TBll, TDal> : IEntityService<TBll>
    where TBll : BllPerson
    where TDal : class, IEntity, new()
{
    protected EntityContext<TDal> Context { get; }

    protected EntityService(EntityContext<TDal> context) => Context = context;

    public bool IsOpen => Context.IsOpen;

    public string? FilePath => Context.FilePath;

    protected abstract TDal ToDal(TBll model);

    protected abstract TBll ToBll(TDal entity);

    protected virtual void Validate(TBll model)
    {
        if (string.IsNullOrWhiteSpace(model.Surname))
            throw new InvalidEntityException("Прізвище не може бути порожнім.");
        if (string.IsNullOrWhiteSpace(model.Name))
            throw new InvalidEntityException("Ім'я не може бути порожнім.");
        if (string.IsNullOrWhiteSpace(model.Key))
            throw new InvalidEntityException("Ключ (номер квитка / id) не може бути порожнім.");
    }

    public void Open(string path, StorageFormat format) =>
        Execute(() => Context.Open(path, ToSerializationType(format)));

    public IReadOnlyList<TBll> GetAll() =>
        Get(() => Context.Items.Select(ToBll).ToList());

    public void Add(TBll item)
    {
        Validate(item);
        Execute(() =>
        {
            if (Context.Find(item.Key) != null)
                throw new DuplicateEntityException(item.Key);
            Context.Add(ToDal(item));
        });
    }

    public void Remove(string key) =>
        Execute(() =>
        {
            if (!Context.Remove(key))
                throw new EntityNotFoundException(key);
        });

    public TBll Find(string key) =>
        Get(() => Context.Find(key) is { } entity
            ? ToBll(entity)
            : throw new EntityNotFoundException(key));

    public string Sing(string key) => Find(key).Sing();

    public void Save() => Execute(Context.Save);

    public void Close() => Context.Close();

    public void DeleteFile() => Execute(Context.DeleteFile);

    protected void Execute(Action action)
    {
        try
        {
            action();
        }
        catch (DataAccessException ex)
        {
            throw new StorageException(ex.Message, ex);
        }
    }

    protected T Get<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (DataAccessException ex)
        {
            throw new StorageException(ex.Message, ex);
        }
    }

    private static SerializationType ToSerializationType(StorageFormat format) => format switch
    {
        StorageFormat.Binary => SerializationType.Binary,
        StorageFormat.Xml => SerializationType.Xml,
        StorageFormat.Json => SerializationType.Json,
        StorageFormat.Custom => SerializationType.Custom,
        _ => throw new InvalidEntityException($"Невідомий формат: {format}.")
    };
}
