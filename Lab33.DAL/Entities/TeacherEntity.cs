using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Lab33.DAL.Entities;

[Serializable]
public class TeacherEntity : IEntity
{
    public string Id { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Name { get; set; } = "";
    public string Subject { get; set; } = "";

    [JsonIgnore, XmlIgnore]
    public string Key => Id;
}
