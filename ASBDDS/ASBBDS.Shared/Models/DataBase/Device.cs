using System;

namespace ASBBDS.Library.Models.DataBase
{
    public class Device : DbBaseModel
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string BaseModel { get; set; }
        public string Serial { get; set; }
        public string MacAddress { get; set; }
        public virtual Switch Switch { get; set; }
    }
}
