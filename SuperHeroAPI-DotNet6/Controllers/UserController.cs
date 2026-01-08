using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Reponse;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize]
        [HttpGet("token-no-role")]
        public async Task<ActionResult<List<UserDTO>>> GetALlUsersAsync()
        {
            return Ok(new ApiResponse<UserDTO>
                (
            message: "All users successfully fetched",
                    payload: null
                ));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("token-admin-role")]
        public async Task<ActionResult<List<UserDTO>>> GetALlUsersAdminAsync()
        {
            return Ok(new ApiResponse<UserDTO>
                (
            message: "All users successfully fetched",
                    payload: null
                ));
        }

        [Authorize(Policy = "AdminOrSubAdmin")]
        [HttpGet("token-admin-subadmin-role")]
        public async Task<ActionResult<List<UserDTO>>> GetALlUsersAdminOrSubAdminAsync()
        {
            return Ok(new ApiResponse<UserDTO>
                (
            message: "All users successfully fetched",
                    payload: null
                ));
        }
    }
}
