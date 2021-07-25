using System;
using System.ComponentModel.DataAnnotations;

namespace ASBBDS.Library.Models.DataBase
{
    public class DbBaseModel
    {
        public DbBaseModel()
        {
            Id = new Guid();
            CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime LastAccessed { get; set; }
    }
}
