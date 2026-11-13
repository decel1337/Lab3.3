using Lab33.BLL.Models;
using Lab33.BLL.Services;

namespace Lab33.PL;

public class AstronautMenu : EntityMenu<BllAstronaut>
{
    public AstronautMenu(AstronautService service) : base(service) { }

    protected override string Title => "Астронавти";

    protected override BllAstronaut ReadEntity() => new()
    {
        Id = ConsoleInput.ReadString("Id"),
        Surname = ConsoleInput.ReadString("Прізвище"),
        Name = ConsoleInput.ReadString("Ім'я"),
        Mission = ConsoleInput.ReadString("Місія")
    };

    protected override string Describe(BllAstronaut a) =>
        $"{a.Id}: {a.Surname} {a.Name}, місія: {a.Mission}";
}
