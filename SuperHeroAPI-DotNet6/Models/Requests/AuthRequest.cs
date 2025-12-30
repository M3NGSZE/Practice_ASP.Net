namespace SuperHeroAPI_DotNet6.Models.Requests
{
    public class AuthRequest
    {
        public String Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
