using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceAdminPutRequest
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        [JsonPropertyName("switchId")]
        [XmlElement(ElementName = "switchId")]
        public Guid? SwitchGuid { get; set; }
        public virtual SwitchPort SwitchPort { get; set; }
        [JsonPropertyName("projectId")]
        [XmlElement(ElementName = "projectId")]
        public virtual Guid ProjectGuid { get; set; }
    }
}
