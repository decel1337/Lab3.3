namespace Lab33.BLL;

// Формат файлу, який бачить рівень представлення (власний тип, щоб PL не залежав від DAL).
public enum StorageFormat
{
    Binary,
    Xml,
    Json,
    Custom
}
