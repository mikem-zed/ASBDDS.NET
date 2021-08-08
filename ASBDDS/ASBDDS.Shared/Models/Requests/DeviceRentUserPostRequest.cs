using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class DeviceRentUserPostRequest
    {
        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public string IPXEUrl { get; set; }
    }
}
