using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Services.Interfaces;

namespace SuperHeroAPI_DotNet6.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public Task<AuthDTO> LoginAsync(AuthRequest authRequest)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> RegisterAsync(UserRequest userRequest)
        {
            throw new NotImplementedException();
        }
    }
}
