using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Lab33.DAL.Entities;

[Serializable]
public class AstronautEntity : IEntity
{
    public string Id { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Name { get; set; } = "";
    public string Mission { get; set; } = "";

    [JsonIgnore, XmlIgnore]
    public string Key => Id;
}
