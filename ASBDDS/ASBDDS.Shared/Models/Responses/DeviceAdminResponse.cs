using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class DeviceAdminResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public Guid SwitchPortId { get; set; }

        public DeviceAdminResponse(Device _device)
        {
            Id = _device.Id;
            Name = _device.Name;
            Model = _device.Model;
            BaseModel = _device.BaseModel;
            Serial = _device.Serial;
            MacAddress = _device.MacAddress;
            SwitchPortId = _device.SwitchPort.Id;
        }

        public DeviceAdminResponse() { }
    }
}
