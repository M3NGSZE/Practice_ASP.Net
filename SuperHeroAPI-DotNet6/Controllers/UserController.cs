using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Reponse;
using SuperHeroAPI_DotNet6.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHeroAPI_DotNet6.Controllers
{
    [SwaggerTag("Controller Test Token And Role Access Restriction")]
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

        [SwaggerOperation(
            Summary = "Admin and Subadmin only endpoint",
            Description = "This endpoint is only accessible by Admin and Subadmin users"
        )]
        [Authorize(Policy = "AdminOrSubAdmin")]
        [HttpGet("token3")]
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
