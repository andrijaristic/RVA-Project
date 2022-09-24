using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.LogsDto;
using Server.Dto.UserDto;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogging _logger;
        
        public UsersController(IUserService userService, ILogging logger)
        {
            _userService = userService;
            _logger = logger;   
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
                ErrorDTO error = new ErrorDTO() { Title = "Login error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                DisplayUserDTO user = await _userService.RegisterNewUser(registerDTO);
                return Ok(user); 
            } catch (Exception e)
            {
                ErrorDTO error = new ErrorDTO() { Title = "Registration error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpPut]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody] UpdateDTO updateDTO)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Updating user information", Enums.ELogType.INFO);
                updateDTO.Username = User.Identity.Name;
                DisplayUserDTO user = await _userService.UpdateUser(updateDTO);
                return Ok(user);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Profile update error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpGet("logs")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting all user logs", Enums.ELogType.INFO);
                List<LogDTO> logs = await _userService.GetLogs(User.Identity.Name);
                return Ok(logs);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Log fetch error", Message = e.Message};
                return BadRequest(error);
            }
        }
    }
}
