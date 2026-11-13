using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Lab33.DAL.Entities;

[Serializable]
public class StudentEntity : IEntity
{
    public string Surname { get; set; } = "";
    public string Name { get; set; } = "";
    public int Course { get; set; }
    public string TicketNumber { get; set; } = "";
    public DateTime BirthDate { get; set; }

    [JsonIgnore, XmlIgnore]
    public string Key => TicketNumber;
}
