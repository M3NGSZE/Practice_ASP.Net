using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Reponse;
using SuperHeroAPI_DotNet6.Models.Reponses;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Services.Implementations;
using SuperHeroAPI_DotNet6.Services.Interfaces;

namespace SuperHeroAPI_DotNet6.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> RegisterUser(UserRequest userRequest)
        {
            return Ok(new ApiResponse<UserDTO>
                (
                    message: "A new user successfully created",
                    statusCode: 201,
                    payload: await _authService.RegisterAsync(userRequest)
                ));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthDTO>>> Login(AuthRequest authRequest)
        {
            return Ok(new ApiResponse<AuthDTO>
                (
                    message: "Login Successfully",
                    payload: await _authService.LoginAsync(authRequest)
                )) ;
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenReponse>> RefreshToken(RefreshTokenRequest tokenRequest)
        {
            return Ok(new ApiResponse<TokenReponse>
               (
                   message: "Login Successfully",
                   statusCode: 201,
                   payload: await _authService.RefreshTokenAsync(tokenRequest)
               ));
        }
    }
}
