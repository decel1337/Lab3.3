using System.Globalization;

namespace Lab33.PL;

// Усе читання з консолі з повторним запитом при некоректному вводі.
public static class ConsoleInput
{
    public static string ReadString(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var text = Console.ReadLine()?.Trim() ?? "";
            if (allowEmpty || text.Length > 0)
                return text;
            Console.WriteLine("Значення не може бути порожнім.");
        }
    }

    public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            if (int.TryParse(ReadString(prompt), out var value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Введіть ціле число від {min} до {max}.");
        }
    }

    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            var text = ReadString($"{prompt} (дд.мм.рррр)");
            if (DateTime.TryParseExact(text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
            Console.WriteLine("Некоректна дата.");
        }
    }
}
