using SuperHeroAPI_DotNet6.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = nameof(RoleName.User); // "User"

        // Many-to-Many: A Role has many Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
