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

            // In RoleConfiguration.cs
            builder.HasData(
                new Role
                {
                    RoleId = new Guid("11111111-1111-1111-1111-111111111111"), // Fixed GUID
                    Name = "Admin",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Role
                {
                    RoleId = new Guid("22222222-2222-2222-2222-222222222222"),
                    Name = "User",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Role
                {
                    RoleId = new Guid("33333333-3333-3333-3333-333333333333"),
                    Name = "SubAdmin",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            );
        }
    }
}
