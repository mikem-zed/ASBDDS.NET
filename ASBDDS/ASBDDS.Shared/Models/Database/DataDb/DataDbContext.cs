using Microsoft.EntityFrameworkCore;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Switch> Switches { get; set; }
        public DbSet<SwitchPort> SwitchPorts { get; set; }
        public DbSet<Router> Routers { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
