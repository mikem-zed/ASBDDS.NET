using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceUserPostRequest
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Device model
        /// </summary>
        public string Model { get; set; }
    }
}
