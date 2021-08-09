using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class DeviceRentUserResponse
    {
        private Device device { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatorId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Closed { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public string IPXEUrl { get; set; }

        public DeviceRentUserResponse(DeviceRent deviceRent)
        {
            device = deviceRent.Device;
            Id = deviceRent.Id;
            Name = deviceRent.Name;
            CreatorId = deviceRent.Creator.Id;
            ProjectId = deviceRent.Project.Id;
            Created = deviceRent.Created;
            Closed = deviceRent.Closed;
            IPXEUrl = deviceRent.IpxeUrl;
            Serial = deviceRent.Device.Serial;
            MacAddress = deviceRent.Device.MacAddress;
        }

        public DeviceRentUserResponse() { }
    }
}
