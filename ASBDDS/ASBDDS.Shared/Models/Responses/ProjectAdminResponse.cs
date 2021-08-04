using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class DeviceLimitResponse
    {
        public string Model { get; set; }
        public int Count { get; set; }

        public DeviceLimitResponse(ProjectDeviceLimit deviceLimit)
        {
            Model = deviceLimit.Model;
            Count = deviceLimit.Count;
        }
    }

    public class ProjectAdminResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultVlan { get; set; }
        public bool AllowCustomBootloaders { get; set; }
        public List<DeviceLimitResponse> DeviceLimits { get; set; }

        public ProjectAdminResponse(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            DefaultVlan = project.DefaultVlan;
            AllowCustomBootloaders = project.AllowCustomBootloaders;
            DeviceLimits = new List<DeviceLimitResponse>();
            foreach(var deviceLimit in project.ProjectDeviceLimits)
            {
                DeviceLimits.Add(new DeviceLimitResponse(deviceLimit));
            }
        }
    }
}
