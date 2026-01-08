namespace SuperHeroAPI_DotNet6.Models.Dtos
{
    public class AuthDTO
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public List<string> roles { get; set; } = new List<string>();

        public string AccessToken { get; set; } = string.Empty;

        public required string RefreshToken { get; set; }
    }
}
