using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceUserPostRequest
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public Guid SwitchPortId { get; set; }
    }
}
