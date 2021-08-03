using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class SwitchPortPostRequest
    {
        public string Number { get; set; }
        public SwitchPortType Type { get; set; }
    }

    public class SwitchPostRequest
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public virtual List<SwitchPortPostRequest> Ports { get; set; }
    }
}
