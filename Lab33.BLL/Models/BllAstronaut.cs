namespace Lab33.BLL.Models;

public class BllAstronaut : BllPerson
{
    public string Id { get; set; } = "";
    public string Mission { get; set; } = "";

    public override string Key => Id;

    public override string Sing() => $"Астронавт {Surname} {Name} співає на орбіті (місія {Mission}).";
}
