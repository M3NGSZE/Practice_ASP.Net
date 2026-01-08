namespace SuperHeroAPI_DotNet6.Models.Reponses
{
    public class TokenReponse
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
