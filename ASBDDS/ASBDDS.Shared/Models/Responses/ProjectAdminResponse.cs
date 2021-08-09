using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class DeviceLimitResponse
    {
        /// <summary>
        /// Device manufacturer
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Device model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Number of devices
        /// </summary>
        public int Count { get; set; }

        public DeviceLimitResponse()
        {

        }

        public DeviceLimitResponse(ProjectDeviceLimit deviceLimit)
        {
            Model = deviceLimit.Model;
            Count = deviceLimit.Count;
            Manufacturer = deviceLimit.Manufacturer;
        }
    }

    public class ProjectAdminResponse
    {
        /// <summary>
        /// Project ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Project default vlan
        /// </summary>
        public int DefaultVlan { get; set; }
        /// <summary>
        /// Allow custom bootloader
        /// </summary>
        public bool AllowCustomBootloaders { get; set; }
        /// <summary>
        /// List of devices limits
        /// </summary>
        public List<DeviceLimitResponse> DeviceLimits { get; set; }
        /// <summary>
        /// Is project disabled
        /// </summary>
        public bool Disabled { get; set; }

        public ProjectAdminResponse() { }

        public ProjectAdminResponse(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            DefaultVlan = project.DefaultVlan;
            AllowCustomBootloaders = project.AllowCustomBootloaders;
            Disabled = project.Disabled;
            DeviceLimits = new List<DeviceLimitResponse>();
            foreach (var deviceLimit in project.ProjectDeviceLimits)
            {
                DeviceLimits.Add(new DeviceLimitResponse(deviceLimit));
            }
        }
    }
}
