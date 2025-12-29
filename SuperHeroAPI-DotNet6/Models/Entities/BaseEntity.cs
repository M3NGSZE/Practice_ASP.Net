using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class BaseEntity
    {
        [Column("created_at")]
        public DateTimeOffset? CreatedAt = DateTimeOffset.UtcNow;

        [Column("updated_at")]
        public DateTimeOffset UpdatedAT = DateTimeOffset.UtcNow;
    }
}
