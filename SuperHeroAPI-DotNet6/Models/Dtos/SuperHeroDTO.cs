namespace SuperHeroAPI_DotNet6.Models.Dtos
{
    public class SuperHeroDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
