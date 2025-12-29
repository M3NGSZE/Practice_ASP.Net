using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet6.Data.Configurations;
using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Single line — applies BaseEntityConfiguration, UserConfiguration, RoleConfiguration, and ALL future ones
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);  // Always call base if overriding
        }
    }
}
