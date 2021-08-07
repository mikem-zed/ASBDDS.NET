using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class SwitchAdminPostRequest
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Ip { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SwitchAccessProtocol AccessProtocol { get; set; }
        public SwitchAuthMethod AuthMethod { get; set; }

        public virtual List<SwitchPortAdminResponse> Ports { get; set; }
    }
}
