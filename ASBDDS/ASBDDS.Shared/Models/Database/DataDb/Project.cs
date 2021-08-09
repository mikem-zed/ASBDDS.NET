using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class Project : DbBaseModel
    {
        public bool Disabled { get; set; }
        public string Name { get; set; }
        public int DefaultVlan { get; set; }
        public bool AllowCustomBootloaders { get; set; } = false;
        public virtual ApplicationUser Creator { get; set; }
        public virtual List<ProjectDeviceLimit> ProjectDeviceLimits { get; set; }
    }
}
