using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("Api/v1")]
    public class UserController : Controller
    {
        private readonly UserProfileService _userProfileService;
        private readonly UserSingInService _userSingInService;
        private readonly UserSingUpService _userSingUpService;

        public UserController(UserSingInService userSingInService, UserSingUpService userSingUpService, UserProfileService userProfileService)
        {
            _userSingInService = userSingInService;
            _userSingUpService = userSingUpService;
            _userProfileService = userProfileService;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount([FromBody] UserDTO userDTO)
        {
            try
            {
                var response = await _userSingUpService.SingUp(userDTO);

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
                var response = await _userSingInService.SingIn(userDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpDelete("delete-user")]
        [Authorize]
        public async Task<ActionResult> DeleteAccount()
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

        [HttpPut("change-information")]
        [Authorize]
        public async Task<ActionResult> ChangeUserInformation([FromBody] ChengeAccountDTO chengeAccountDTO)
        {
            try
            {
                var tokenUserID = User.FindFirst("UserId")?.Value;

                if(tokenUserID == null)
                {
                    return BadRequest("Usuario sem autorização");
                }

                var userId = Guid.Parse(tokenUserID);
                var response = await _userProfileService.ChangeUserInformation(userId ,chengeAccountDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}