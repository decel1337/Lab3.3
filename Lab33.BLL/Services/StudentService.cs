using Lab33.BLL.Exceptions;
using Lab33.BLL.Models;
using Lab33.DAL;
using Lab33.DAL.Entities;

namespace Lab33.BLL.Services;

public class StudentService : EntityService<BllStudent, StudentEntity>
{
    public StudentService() : this(new EntityContext<StudentEntity>()) { }

    public StudentService(EntityContext<StudentEntity> context) : base(context) { }

    protected override StudentEntity ToDal(BllStudent m) => new()
    {
        Surname = m.Surname,
        Name = m.Name,
        Course = m.Course,
        TicketNumber = m.TicketNumber,
        BirthDate = m.BirthDate
    };

    protected override BllStudent ToBll(StudentEntity e) => new()
    {
        Surname = e.Surname,
        Name = e.Name,
        Course = e.Course,
        TicketNumber = e.TicketNumber,
        BirthDate = e.BirthDate
    };

    protected override void Validate(BllStudent m)
    {
        base.Validate(m);
        if (m.Course is < 1 or > 6)
            throw new InvalidEntityException("Курс має бути від 1 до 6.");
        if (m.BirthDate > DateTime.Today || m.BirthDate.Year < 1900)
            throw new InvalidEntityException("Некоректна дата народження.");
    }

    // Студенти 3-го курсу, народжені влітку; дані перечитуються з файлу.
    public IReadOnlyList<BllStudent> GetThirdYearSummerStudents() =>
        Get(() =>
        {
            Context.Reload();
            return Context.Items
                .Select(ToBll)
                .Where(s => s.Course == 3 && s.IsBornInSummer())
                .ToList();
        });

    public int CountThirdYearSummerStudents() => GetThirdYearSummerStudents().Count;
}
