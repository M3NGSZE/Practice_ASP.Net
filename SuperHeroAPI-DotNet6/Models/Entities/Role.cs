using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class Role : BaseEntity
    {
        [Column("role_id")]
        public required Guid RoleId { get; set; } = Guid.NewGuid();

        [Column("role_name")]
        [StringLength(50)]                         // Optional: limit length
        public string Name { get; set; } = nameof(RoleName.User); // "User"

        [Column("is_active")]
        public bool IsActive { get; set; }
    }

    enum RoleName
    {
        Admin,
        SubAdmin,
        User
    }
}
