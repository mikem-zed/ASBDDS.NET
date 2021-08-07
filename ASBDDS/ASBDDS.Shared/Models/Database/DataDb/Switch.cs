using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public enum SwitchAuthMethod
    {
        SSH_USER_PASS
    }

    public enum SwitchAccessProtocol
    {
        SSH,
        Telnet
    }

    public class Switch : DbBaseModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Serial { get; set; }
        public string Ip { get; set; }
        public SwitchAccessProtocol AccessProtocol { get; set; }
        public SwitchAuthMethod AuthMethod { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual Router Router { get; set; }
        public virtual Guid? RouterGuid { get => Router?.Id; }
        public virtual List<SwitchPort> Ports { get; set; }
    }
}
