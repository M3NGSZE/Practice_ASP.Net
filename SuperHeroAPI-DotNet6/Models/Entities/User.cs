using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class User : BaseEntity
    {
        [Column("user_id")]
        public required Guid UserId { get; set; } = Guid.NewGuid();

        [Column("email")]
        public required String Email { get; set; }

        [Column("username")]
        public required string UserName { get; set; }

        [Column("password")]
        public required string Password { get; set; }

    }
}
