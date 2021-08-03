using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceAdminPostRequest
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public Guid SwitchPortId { get; set; }
    }
}
