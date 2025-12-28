namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class BaseEntity
    {
        public DateTimeOffset? CreatedAt = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAT = DateTimeOffset.UtcNow;
    }
}
