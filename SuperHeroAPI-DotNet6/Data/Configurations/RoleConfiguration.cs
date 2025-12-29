using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table & PK
            builder.ToTable("roles");
            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleId)
                   .HasColumnName("role_id")
                   .ValueGeneratedNever();

            // Properties
            builder.Property(r => r.Name)
                   .HasColumnName("role_name")
                   .IsRequired()
                   .HasMaxLength(50);

            // Unique role names
            builder.HasIndex(r => r.Name).IsUnique();
        }
    }
}
