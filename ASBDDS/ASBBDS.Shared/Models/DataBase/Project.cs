using System.Collections.Generic;

namespace ASBBDS.Library.Models.DataBase
{
    public class Project : DbBaseModel
    {
        public int DefaultVlan { get; set; }
        public virtual List<ProjectDeviceLimit> ProjectDeviceLimits { get; set; }
    }
}
