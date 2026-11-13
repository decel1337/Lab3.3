namespace Lab33.BLL.Models;

public class BllTeacher : BllPerson
{
    public string Id { get; set; } = "";
    public string Subject { get; set; } = "";

    public override string Key => Id;

    public override string Sing() => $"Вчитель {Surname} {Name} ({Subject}) співає на уроці.";
}
