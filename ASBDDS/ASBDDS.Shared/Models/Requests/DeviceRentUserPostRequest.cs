using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceRentUserPostRequest
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid ProjectId { get; set; }
        public string IPXEUrl { get; set; }
    }
}
