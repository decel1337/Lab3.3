using Lab33.BLL;
using Lab33.BLL.Exceptions;
using Lab33.BLL.Models;
using Lab33.BLL.Services;

namespace Lab33.PL;

// Загальне меню роботи з одним типом сутності. Усі винятки BLL перехоплюються тут
// і перетворюються на повідомлення для користувача.
public abstract class EntityMenu<T> where T : BllPerson
{
    protected IEntityService<T> Service { get; }

    protected EntityMenu(IEntityService<T> service) => Service = service;

    protected abstract string Title { get; }

    protected abstract T ReadEntity();

    protected abstract string Describe(T item);

    // Додаткові пункти меню конкретної сутності.
    protected virtual IEnumerable<(string Caption, Action Action)> ExtraItems() => Array.Empty<(string, Action)>();

    public void Run()
    {
        while (true)
        {
            var items = new List<(string Caption, Action Action)>
            {
                ("Відкрити / створити файл", OpenFile),
                ("Показати всі записи", ShowAll),
                ("Додати запис", AddItem),
                ("Видалити запис", RemoveItem),
                ("Знайти запис", FindItem),
                ("Заспівати", SingItem),
            };
            items.AddRange(ExtraItems());
            items.Add(("Зберегти у файл", () => Service.Save()));
            items.Add(("Закрити файл", () => Service.Close()));
            items.Add(("Видалити файл", DeleteFile));

            Console.WriteLine($"\n=== {Title} ({(Service.IsOpen ? Service.FilePath : "файл не відкрито")}) ===");
            for (int i = 0; i < items.Count; i++)
                Console.WriteLine($"{i + 1}. {items[i].Caption}");
            Console.WriteLine("0. Назад");

            int choice = ConsoleInput.ReadInt("Ваш вибір", 0, items.Count);
            if (choice == 0)
                return;

            Safe(items[choice - 1].Action);
        }
    }

    protected static void Safe(Action action)
    {
        try
        {
            action();
        }
        catch (LabException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    private void OpenFile()
    {
        var path = ConsoleInput.ReadString("Ім'я файлу (розширення можна не вказувати)");
        Console.WriteLine("Формат: 1 - бінарний, 2 - XML, 3 - JSON, 4 - користувацький");
        var format = (StorageFormat)(ConsoleInput.ReadInt("Формат", 1, 4) - 1);
        Service.Open(path, format);
        Console.WriteLine($"Відкрито: {Service.FilePath}");
    }

    private void ShowAll()
    {
        var all = Service.GetAll();
        if (all.Count == 0)
            Console.WriteLine("Записів немає.");
        foreach (var item in all)
            Console.WriteLine(Describe(item));
    }

    private void AddItem()
    {
        Service.Add(ReadEntity());
        Console.WriteLine("Додано (не забудьте зберегти у файл).");
    }

    private void RemoveItem()
    {
        Service.Remove(ConsoleInput.ReadString("Ключ запису"));
        Console.WriteLine("Видалено (не забудьте зберегти у файл).");
    }

    private void FindItem() =>
        Console.WriteLine(Describe(Service.Find(ConsoleInput.ReadString("Ключ запису"))));

    private void SingItem() =>
        Console.WriteLine(Service.Sing(ConsoleInput.ReadString("Ключ запису")));

    private void DeleteFile()
    {
        Service.DeleteFile();
        Console.WriteLine("Файл видалено.");
    }
}
