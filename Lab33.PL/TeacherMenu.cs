using Lab33.BLL.Models;
using Lab33.BLL.Services;

namespace Lab33.PL;

public class TeacherMenu : EntityMenu<BllTeacher>
{
    public TeacherMenu(TeacherService service) : base(service) { }

    protected override string Title => "Вчителі";

    protected override BllTeacher ReadEntity() => new()
    {
        Id = ConsoleInput.ReadString("Id"),
        Surname = ConsoleInput.ReadString("Прізвище"),
        Name = ConsoleInput.ReadString("Ім'я"),
        Subject = ConsoleInput.ReadString("Предмет")
    };

    protected override string Describe(BllTeacher t) =>
        $"{t.Id}: {t.Surname} {t.Name}, предмет: {t.Subject}";
}
