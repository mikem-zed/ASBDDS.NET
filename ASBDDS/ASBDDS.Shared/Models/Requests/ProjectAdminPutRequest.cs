using ASBDDS.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class ProjectAdminPutRequest
    {
        /// <summary>
        /// Project name
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
        /// Is project disabled
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// List of device limits
        /// Devices that are in the project, but not in the request, will be deleted
        /// </summary>
        public virtual List<DeviceLimitResponse> ProjectDeviceLimits { get; set; }
    }
}
