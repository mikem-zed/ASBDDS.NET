using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class DeviceUserResponse
    {
        /// <summary>
        /// Device ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Device name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Device model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Device base model
        /// </summary>
        public string BaseModel { get; set; }
        /// <summary>
        /// Device serial number
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// Device mac address
        /// </summary>
        public string MacAddress { get; set; }
        /// <summary>
        /// The project ID to which device is attached
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Device state
        /// </summary>
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
