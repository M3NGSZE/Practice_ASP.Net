using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDTO> LoginAsync(string email, string password);
        Task<UserDTO> RegisterAsync(UserRequest userRequest);
    }
}
