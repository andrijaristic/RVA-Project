using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.LogsDto;
using Server.Dto.UserDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                AuthDTO authDTO = await _userService.Login(loginDTO);
                return Ok(authDTO);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                User user = await _userService.RegisterNewUser(registerDTO);
                return Ok(user); 
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody] UpdateDTO updateDTO)
        {
            try
            {
                updateDTO.Username = User.Identity.Name;
                User user = await _userService.UpdateUser(updateDTO);
                return Ok(user);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logs")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                List<LogDTO> logs = await _userService.GetLogs(User.Identity.Name);
                return Ok(logs);
            } catch (Exception e)
            {
                ErrorDTO error = new ErrorDTO() { Title = "Log fetch error", Message = e.Message};
                return BadRequest(error);
            }
        }
    }
}
