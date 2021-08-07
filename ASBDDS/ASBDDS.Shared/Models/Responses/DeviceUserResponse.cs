using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class DeviceUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public Guid ProjectId { get; set; }
        public string State { get; set; }

        public DeviceUserResponse(Device _device)
        {

            Name = _device.Name;
            Model = _device.Model;
            Serial = _device.Serial;
            MacAddress = _device.MacAddress;
            State = _device.StateEnum.ToString();
        }
    }
}
