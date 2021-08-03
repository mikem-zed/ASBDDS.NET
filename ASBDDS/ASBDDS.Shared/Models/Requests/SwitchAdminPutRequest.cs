using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class SwitchAdminPortPutRequest
    {
        public string Number { get; set; }
        public SwitchPortType Type { get; set; }
    }

    public class SwitchAdminPutRequest
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public virtual List<SwitchAdminPortPutRequest> Ports { get; set; }
    }
}
