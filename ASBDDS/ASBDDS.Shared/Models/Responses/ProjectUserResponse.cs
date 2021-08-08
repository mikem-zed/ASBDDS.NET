using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class ProjectUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
