namespace SuperHeroAPI_DotNet6.Models.Requests
{
    public class UserRequest
    {
        public String Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
