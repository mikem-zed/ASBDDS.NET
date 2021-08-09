using ASBDDS.Shared.Models.Responses;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class ProjectDeviceLimit : DbBaseModel
    {
        public Project Project { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
        public ProjectDeviceLimit() {}

        public ProjectDeviceLimit(DeviceLimitResponse limitRequest, Project project)
        {
            Manufacturer = limitRequest.Manufacturer;
            Model = limitRequest.Model;
            Count = limitRequest.Count;
            Project = project;
        }
    }
}