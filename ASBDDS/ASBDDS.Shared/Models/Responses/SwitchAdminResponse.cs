using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class SwitchAdminResponse
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Ip { get; set; }
        public virtual Guid? RouterId { get; set; }
        public virtual List<Guid> PortsIds { get; set; }

        public SwitchAdminResponse(Switch _switch)
        {
            Name = _switch.Name;
            Serial = _switch.Serial;
            Ip = _switch.Ip;
            RouterId = _switch.RouterGuid;
            
            var _portsIds = new List<Guid>();
            foreach(var port in _switch.Ports)
            {
                _portsIds.Add(port.Id);
            }
            PortsIds = _portsIds;
        }
    }
}
