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
        /// Link to IPXE cfg network bootloader
        /// </summary>
        public string IPXEUrl { get; set; }
    }
}
