namespace SuperHeroAPI_DotNet6.Models.Dtos
{
    public class AuthDTO
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public List<string> roles { get; set; } = new List<string>();
    }
}
