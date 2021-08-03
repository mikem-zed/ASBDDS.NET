using System.Collections.Generic;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class Router : DbBaseModel
    {
        public virtual List<Switch> Switches { get; set; }
    }
}
