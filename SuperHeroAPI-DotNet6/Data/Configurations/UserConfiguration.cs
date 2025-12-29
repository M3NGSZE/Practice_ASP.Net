using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table & PK
            builder.ToTable("users");
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                   .HasColumnName("user_id")
                   .ValueGeneratedNever(); // Manual GUID

            // Properties
            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired()
                   .HasMaxLength(255)
                   .IsUnicode(false);

            builder.Property(u => u.UserName)
                   .HasColumnName("user_name")
                   .IsRequired()
                   .HasMaxLength(100);


            builder.Property(u => u.Password)
                   .HasColumnName("password_hash")  // Better name for hashed password
                   .IsRequired();

            // Unique constraints
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();


            // PostgreSQL-friendly CHECK constraint
            // Rules:
            // - Must contain at least one @
            // - No whitespace (spaces, tabs, etc.)
            // - No double @@
            builder.ToTable("users", tb => tb
                .HasCheckConstraint(
                    "CK_users_email_basic",
                    @"email ~ '@'                     
                  AND email !~ '\s'               
                  AND email !~ '@{2}'"
                )
            );

            // Many-to-Many relationship (EF Core 5+ implicit junction)
            builder.HasMany(u => u.Roles)
                   .WithMany(r => r.Users)
                   .UsingEntity(j =>
                   {
                       j.ToTable("user_roles");           // Custom junction table name

                       // Junction table columns
                       j.Property<Guid>("user_id");
                       j.Property<Guid>("role_id");

                       // Composite primary key
                       j.HasKey("user_id", "role_id");

                       // Indexes for performance
                       j.HasIndex("user_id");
                       j.HasIndex("role_id");
                   });
        }
    }
}
