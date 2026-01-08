using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Reponses;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDTO> LoginAsync(AuthRequest authRequest);

        Task<UserDTO> RegisterAsync(UserRequest userRequest);

        Task<TokenReponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
