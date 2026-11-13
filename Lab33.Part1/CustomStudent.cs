using System.Runtime.Serialization;

namespace Lab33.Part1;

// Користувацька серіалізація: клас сам керує тим, що і як записується (ISerializable).
// Тут номер групи зберігається у вигляді двох полів: курс та номер групи на курсі.
[Serializable]
public class CustomStudent : Student, ISerializable
{
    public CustomStudent() { }

    public CustomStudent(Student s) : base(s.TicketNumber, s.FullName, s.BirthYear, s.GroupNumber) { }

    // Конструктор десеріалізації
    protected CustomStudent(SerializationInfo info, StreamingContext context)
    {
        TicketNumber = info.GetString("ticket") ?? "";
        FullName = info.GetString("fullName") ?? "";
        BirthYear = info.GetInt32("birthYear");
        GroupNumber = info.GetInt32("course") * 10 + info.GetInt32("groupInCourse");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("ticket", TicketNumber);
        info.AddValue("fullName", FullName);
        info.AddValue("birthYear", BirthYear);
        info.AddValue("course", GroupNumber / 10);
        info.AddValue("groupInCourse", GroupNumber % 10);
    }
}
