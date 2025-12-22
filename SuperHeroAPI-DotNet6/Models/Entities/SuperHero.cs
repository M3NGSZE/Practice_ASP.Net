using SuperHeroAPI_DotNet6.Models.Dtos;

namespace SuperHeroAPI_DotNet6.Models.Entities
{
    public class SuperHero
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;

        public static implicit operator SuperHero(SuperHeroDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
