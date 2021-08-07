using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class DeviceRent : DbBaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Project Project { get; set; }
        public DateTime? Closed { get; set; }
        public Guid UserId { get; set; }
        public virtual Device Device { get; set; }
        public string IPXEUrl { get; set; }
    }
}
