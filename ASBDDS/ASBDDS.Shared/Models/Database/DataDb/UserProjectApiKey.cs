using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class UserProjectApiKey
    {
        public virtual Project Project { get; set; }
        public Guid UserGuid { get; set; }
        public bool ReadOnly { get; set; } = true;
    }
}