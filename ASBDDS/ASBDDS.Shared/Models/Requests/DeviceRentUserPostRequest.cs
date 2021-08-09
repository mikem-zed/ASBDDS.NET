using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceRentUserPostRequest
    {
        /// <summary>
        /// Device model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Device manufacturer
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Device name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The ID of the user who created rent
        /// </summary>
        public Guid? CreatorId { get; set; }
        /// <summary>
        /// The project ID to which it is attached
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Link to IPXE network bootloader
        /// </summary>
        public string IPXEUrl { get; set; }
    }
}
