using ASBDDS.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class ProjectAdminPostRequest
    {
        /// <summary>
        /// /Project name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Default vlan
        /// </summary>
        public int DefaultVlan { get; set; }
        /// <summary>
        /// Allow custom bootloader
        /// </summary>
        public bool AllowCustomBootloaders { get; set; }
        /// <summary>
        /// List of device limits
        /// </summary>
        public virtual List<DeviceLimitResponse> ProjectDeviceLimits { get; set; }
    }
}
