namespace Lab33.DAL;

// Помилка рівня доступу до даних (файл, формат, стан контексту).
public class DataAccessException : Exception
{
    public DataAccessException(string message) : base(message) { }
    public DataAccessException(string message, Exception inner) : base(message, inner) { }
}
