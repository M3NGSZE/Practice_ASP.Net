namespace SuperHeroAPI_DotNet6.Models.Requests
{
    public class RefreshTokenRequest
    {
        public Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
