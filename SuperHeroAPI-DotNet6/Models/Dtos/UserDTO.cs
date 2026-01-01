using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Models.Dtos
{
    public class UserDTO
    {
        public Guid UserId { get; set; } 

        public String Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public List<string> roles { get; set; } = new List<string>();

        //public string Role { get; set; } = string.Empty;
    }
}
