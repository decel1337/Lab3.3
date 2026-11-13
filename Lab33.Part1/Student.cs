namespace Lab33.Part1;

// Варіант 1. Студент: номер квитка, ПІБ, рік народження, номер групи.
// Номер групи кодує курс першою цифрою (21 = 2-й курс, група 1).
// [Serializable] — стандартна (бінарна) серіалізація; публічний конструктор
// без параметрів і публічні властивості — для XML та JSON.
[Serializable]
public class Student
{
    public string TicketNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public int BirthYear { get; set; }
    public int GroupNumber { get; set; }

    public Student() { }

    public Student(string ticketNumber, string fullName, int birthYear, int groupNumber)
    {
        TicketNumber = ticketNumber;
        FullName = fullName;
        BirthYear = birthYear;
        GroupNumber = groupNumber;
    }

    public int GetCourse() => GroupNumber / 10;

    public int CalculateAge() => DateTime.Now.Year - BirthYear;

    // Переведення на наступний курс (перша цифра номера групи збільшується на 1).
    public void PromoteToNextCourse()
    {
        if (GetCourse() >= 6)
            throw new InvalidOperationException("Студент уже на останньому курсі.");
        GroupNumber += 10;
    }

    public void PrintInfo() => Console.WriteLine(ToString());

    public override string ToString() =>
        $"{TicketNumber}: {FullName}, {BirthYear} р.н. ({CalculateAge()} р.), група {GroupNumber}, {GetCourse()} курс";

    public override bool Equals(object? obj) =>
        obj is Student s
        && TicketNumber == s.TicketNumber
        && FullName == s.FullName
        && BirthYear == s.BirthYear
        && GroupNumber == s.GroupNumber;

    public override int GetHashCode() => HashCode.Combine(TicketNumber, FullName, BirthYear, GroupNumber);
}
