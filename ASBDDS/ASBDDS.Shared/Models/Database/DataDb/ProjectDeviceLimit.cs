namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class ProjectDeviceLimit : DbBaseModel
    {
        public virtual Project Project { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
    }
}