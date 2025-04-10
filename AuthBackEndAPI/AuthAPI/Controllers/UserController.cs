using AuthAPI.DTOs;
using AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("Api/v1")]
    public class UserController : Controller
    {
        private readonly UserAuthService _userAuthService;
        private readonly UserProfileService _userProfileService;

        public UserController(UserAuthService userAuthService, UserProfileService userProfileService)
        {
            _userAuthService = userAuthService;
            _userProfileService = userProfileService;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount([FromBody] UserDTO userDTO)
        {
            try
            {
                var response = await _userAuthService.SingUp(userDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPost("login-account")]
        public async Task<ActionResult> LoginAccount([FromBody] UserDTO userDTO)
        {
            try
            {
                var response = await _userAuthService.SingIn(userDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpDelete("delete-user")]
        [Authorize]
        public async Task<ActionResult> GetUserById()
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if (tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);
                var response = await _userProfileService.DeleteUser(userId);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}