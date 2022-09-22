using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogging _logger;
        public StudentsController(IStudentService studentService, ILogging logger)
        {
            _studentService = studentService;
            _logger = logger;
        }
        
        [HttpGet("get-id")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting ID for corresponding student", Enums.ELogType.INFO);

                string username = User.Identity.Name;
                DisplayStudentDTO student = await _studentService.GetStudentId(username);
                return Ok(student);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO err = new ErrorDTO() {Title =  "Error", Message = e.Message};
                return BadRequest(err);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting all registered students", Enums.ELogType.INFO);
                List<DetailedStudentDTO> students = await _studentService.GetStudentsDetailed();
                return Ok(students);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Student fetch error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpPost("duplicate/{id}")]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Duplicate(int id)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Duplicating student with ID [{id}]", Enums.ELogType.INFO);
                DetailedStudentDTO detailedStudentDTO = await _studentService.Duplicate(id);
                return Ok(detailedStudentDTO);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Student duplication error", Message = e.Message};
                return BadRequest(error);
            }
        }
    }
}
