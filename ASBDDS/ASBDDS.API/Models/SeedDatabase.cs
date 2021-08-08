using ASBDDS.Shared.Models.Database.DataDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASBDDS.API.Models
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var dataContext = serviceProvider.GetRequiredService<DataDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
