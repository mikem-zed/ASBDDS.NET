using System;
using System.ComponentModel.DataAnnotations;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class DbBaseModelWithoutId
    {
        public DbBaseModelWithoutId()
        {
            Created = DateTime.UtcNow;
        }
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
    }

    public class DbBaseModel : DbBaseModelWithoutId
    {
        public DbBaseModel()
        {
            Id = new Guid();
            Created = DateTime.UtcNow;
        }

        [Key]
        public virtual Guid Id { get; set; }
    }
}
