using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class Switch : DbBaseModel
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Ip { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual Router Router { get; set; }
        public virtual Guid? RouterGuid { get => Router?.Id; }
        public virtual List<SwitchPort> Ports { get; set; }
    }
}
