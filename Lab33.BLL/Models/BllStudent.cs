namespace Lab33.BLL.Models;

public class BllStudent : BllPerson
{
    public int Course { get; set; }
    public string TicketNumber { get; set; } = "";
    public DateTime BirthDate { get; set; }

    public override string Key => TicketNumber;

    public bool IsBornInSummer() => BirthDate.Month is >= 6 and <= 8;

    public override string Sing() => $"Студент {Surname} {Name} співає в гуртожитку.";
}
