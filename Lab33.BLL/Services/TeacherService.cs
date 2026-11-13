using Lab33.BLL.Models;
using Lab33.DAL;
using Lab33.DAL.Entities;

namespace Lab33.BLL.Services;

public class TeacherService : EntityService<BllTeacher, TeacherEntity>
{
    public TeacherService() : this(new EntityContext<TeacherEntity>()) { }

    public TeacherService(EntityContext<TeacherEntity> context) : base(context) { }

    protected override TeacherEntity ToDal(BllTeacher m) => new()
    {
        Id = m.Id,
        Surname = m.Surname,
        Name = m.Name,
        Subject = m.Subject
    };

    protected override BllTeacher ToBll(TeacherEntity e) => new()
    {
        Id = e.Id,
        Surname = e.Surname,
        Name = e.Name,
        Subject = e.Subject
    };
}
