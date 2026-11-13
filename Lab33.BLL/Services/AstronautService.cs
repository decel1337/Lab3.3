using Lab33.BLL.Models;
using Lab33.DAL;
using Lab33.DAL.Entities;

namespace Lab33.BLL.Services;

public class AstronautService : EntityService<BllAstronaut, AstronautEntity>
{
    public AstronautService() : this(new EntityContext<AstronautEntity>()) { }

    public AstronautService(EntityContext<AstronautEntity> context) : base(context) { }

    protected override AstronautEntity ToDal(BllAstronaut m) => new()
    {
        Id = m.Id,
        Surname = m.Surname,
        Name = m.Name,
        Mission = m.Mission
    };

    protected override BllAstronaut ToBll(AstronautEntity e) => new()
    {
        Id = e.Id,
        Surname = e.Surname,
        Name = e.Name,
        Mission = e.Mission
    };
}
