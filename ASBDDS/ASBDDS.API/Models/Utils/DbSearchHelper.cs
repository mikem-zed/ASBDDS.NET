using ASBDDS.Shared.Models.Database.DataDb;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASBDDS.API.Models.Utils
{
    public static class DbSearchHelper
    {
        public static async Task<Project> FindProject(DataDbContext context, HttpRequest request)
        {
            Project project = null;
            var projIdStrValues = new StringValues();
            if (request.Headers.TryGetValue("ProjectId", out projIdStrValues))
            {
                var projId = projIdStrValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(projId))
                {
                    project = await context.Projects.Where(p => p.Id == Guid.Parse(projId) && !p.Disabled).FirstOrDefaultAsync();
                }
            }
            return project;
        }
    }
}
