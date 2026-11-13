namespace Lab33.BLL.Models;

// Додаткове вміння за варіантом: співати.
public interface ISinger
{
    string Sing();
}

// Спільна основа сутностей рівня BLL.
public abstract class BllPerson : ISinger
{
    public string Surname { get; set; } = "";
    public string Name { get; set; } = "";

    public abstract string Key { get; }

    public virtual string Sing() => $"{Surname} {Name} співає: «Ля-ля-ля!»";
}
