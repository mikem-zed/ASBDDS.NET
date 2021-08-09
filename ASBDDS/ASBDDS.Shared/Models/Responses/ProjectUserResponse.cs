using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class ProjectUserResponse
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
        /// Default vlan
        /// </summary>
        public int DefaultVlan { get; set; }

        public ProjectUserResponse(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            DefaultVlan = project.DefaultVlan;
        }

        public ProjectUserResponse() { }
    }
}
