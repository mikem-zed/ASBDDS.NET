using System;
using GitHub.JPMikkers.DHCP;

namespace ASBDDS.Shared.Models.Responses
{
    public class DHCPLeaseAdminResponse
    {
        public string IP { get; set; }
        public string MacAddress { get; set; }
        public string Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Static { get; set; }
        public DHCPLeaseAdminResponse() {}

        public DHCPLeaseAdminResponse(DHCPLease lease)
        {
            IP = lease.Address.ToString();
            MacAddress = lease.MacAddress;
            Status = lease.Status.ToString().ToLower();
            Start = lease.Start;
            End = lease.End;
            Static = lease.Static;
        }
    }
}