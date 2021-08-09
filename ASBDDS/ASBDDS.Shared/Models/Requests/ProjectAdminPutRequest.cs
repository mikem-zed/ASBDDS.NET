using ASBDDS.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class ProjectAdminPutRequest
    {
        public string Name { get; set; }
        public int DefaultVlan { get; set; }
        public bool AllowCustomBootloaders { get; set; }
        public bool Disabled { get; set; }
        public virtual List<DeviceLimitResponse> ProjectDeviceLimits { get; set; }
    }
}
