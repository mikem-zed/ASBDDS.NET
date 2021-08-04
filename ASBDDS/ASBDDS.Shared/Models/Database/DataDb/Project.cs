using System.Collections.Generic;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class Project : DbBaseModel
    {
        public string Name { get; set; }
        public int DefaultVlan { get; set; }
        public bool AllowCustomBootloaders { get; set; } = false;
        public virtual List<ProjectDeviceLimit> ProjectDeviceLimits { get; set; }
    }
}
