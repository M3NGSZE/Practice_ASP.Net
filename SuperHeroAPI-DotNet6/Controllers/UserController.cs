using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Reponse;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Controllers
{
    /// <summary>
    /// Admin operations controller
    /// </summary>
    /// <remarks>
    /// Only Admin users can access these endpoints.
    /// </remarks>
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

        /// <summary>
        /// Admin endpoint: only accessible by Admin users
        /// </summary>
        /// <remarks>
        /// This endpoint allows Admins to see system overview.
        /// </remarks>
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
