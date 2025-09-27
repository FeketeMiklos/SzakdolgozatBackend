using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SzakdolgozatBackend.Dtos.User;
using SzakdolgozatBackend.Entities;
using SzakdolgozatBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SzakdolgozatBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserGetDto>> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserGetDto>> RegisterUser([FromBody]UserCreateDto userCreateDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(userCreateDto);
                return Ok(user);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentOutOfRangeException e) 
            { 
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            try
            {
                await _userService.LoginAsync(userDto);
                return Ok();
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id, [FromBody]UserPatchDto dto)
        {
            try
            {
                await _userService.UpdateUserDataAsync(id, dto);
                return Ok("Info updated!");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }   
        }

        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] PasswordChangeDto passwordChangeDto)
        {
            try
            {
                await _userService.ChangePasswordAsync(id, passwordChangeDto);
                return Ok("Password change successfull!");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgottenPasswordChange([FromBody] ForgottenPasswordDto forgottenPasswordChangeDto)
        {
            try
            {
                await _userService.ForgottenPasswordChangeAsync(forgottenPasswordChangeDto);
                return Ok("Password change successfull!");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
