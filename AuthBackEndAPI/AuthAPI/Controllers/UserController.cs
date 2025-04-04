using AuthAPI.DTOs;
using AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("Api/v1")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount([FromBody] UserDTO userDTO)
        {
            try
            {
                var response = await _userService.Login(userDTO);

                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}