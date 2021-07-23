using Microsoft.EntityFrameworkCore;

namespace ASBBDS.Library.Models.DataBase
{
    public class DataDbContext: DbContext
    {
        public DataDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Switch> Switches { get; set; }
        public DbSet<Router> Routers { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseNpgsql("Host=localhost;Database=ASBDDS;Username=postgres;Password=903632as");
    }
}
