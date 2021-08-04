using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class SwitchAdminPutRequest
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public virtual List<SwitchPortAdminResponse> Ports { get; set; }
    }
}
