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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all entity configurations from assembly first
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration<>).Assembly);

            // Apply BaseEntityConfiguration to all entities inheriting BaseEntity
            // Note: Generic configurations must be applied explicitly (ApplyConfigurationsFromAssembly doesn't discover them)
            // Apply after other configurations to ensure base entity column names are set correctly
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<User>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Role>());

            base.OnModelCreating(modelBuilder);  // Always call base if overriding
        }
    }
}
