using Lab33.BLL.Models;
using Lab33.BLL.Services;

namespace Lab33.PL;

public class StudentMenu : EntityMenu<BllStudent>
{
    private readonly StudentService _service;

    public StudentMenu(StudentService service) : base(service) => _service = service;

    protected override string Title => "Студенти";

    protected override BllStudent ReadEntity() => new()
    {
        Surname = ConsoleInput.ReadString("Прізвище"),
        Name = ConsoleInput.ReadString("Ім'я"),
        Course = ConsoleInput.ReadInt("Курс", 1, 6),
        TicketNumber = ConsoleInput.ReadString("Номер студентського квитка"),
        BirthDate = ConsoleInput.ReadDate("Дата народження")
    };

    protected override string Describe(BllStudent s) =>
        $"{s.TicketNumber}: {s.Surname} {s.Name}, {s.Course} курс, н. {s.BirthDate:dd.MM.yyyy}";

    protected override IEnumerable<(string Caption, Action Action)> ExtraItems()
    {
        yield return ("Студенти 3-го курсу, народжені влітку", ShowThirdYearSummer);
    }

    private void ShowThirdYearSummer()
    {
        var found = _service.GetThirdYearSummerStudents();
        Console.WriteLine($"Знайдено: {found.Count}");
        foreach (var s in found)
            Console.WriteLine(Describe(s));
    }
}
