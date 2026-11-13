namespace Lab33.BLL.Exceptions;

// Базовий виняток предметної області (студенти, вчителі, астронавти).
public class LabException : Exception
{
    public LabException(string message) : base(message) { }
    public LabException(string message, Exception inner) : base(message, inner) { }
}

public class EntityNotFoundException : LabException
{
    public string Key { get; }

    public EntityNotFoundException(string key)
        : base($"Запис із ключем '{key}' не знайдено.") => Key = key;
}

public class DuplicateEntityException : LabException
{
    public string Key { get; }

    public DuplicateEntityException(string key)
        : base($"Запис із ключем '{key}' уже існує.") => Key = key;
}

public class InvalidEntityException : LabException
{
    public InvalidEntityException(string message) : base(message) { }
}

public class StorageException : LabException
{
    public StorageException(string message, Exception inner) : base(message, inner) { }
}
