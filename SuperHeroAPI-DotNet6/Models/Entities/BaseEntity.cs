using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class BaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
