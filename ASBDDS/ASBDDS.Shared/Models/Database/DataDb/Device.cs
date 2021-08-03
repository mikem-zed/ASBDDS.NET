using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class Device : DbBaseModelWithoutId
    {
        public virtual Guid? Id { get; set; }
        [Key]
        public Guid InternalId { get; set; }
        public string Name { get; set; }
        public DeviceState StateEnum { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public virtual SwitchPort SwitchPort { get; set; }
        [JsonPropertyName("projectId")]
        [XmlElement(ElementName = "projectId")]
        public virtual Guid ProjectGuid { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual Project Project { get; set; }
    }
}
