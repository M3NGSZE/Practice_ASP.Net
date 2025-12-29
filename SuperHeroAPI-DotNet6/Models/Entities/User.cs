using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        public String Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
