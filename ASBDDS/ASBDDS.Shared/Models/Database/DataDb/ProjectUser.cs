using System;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class ProjectUser : DbBaseModel
    {
        public ApplicationUser User { get; set; }
        public virtual Project Project { get; set; }
    }
}