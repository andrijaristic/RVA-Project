using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.StudentDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        
        [HttpGet("get-id")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                string username = User.Identity.Name;
                DisplayStudentDTO student = await _studentService.GetStudentId(username);
                return Ok(student);
            } catch (Exception e)
            {
                ErrorDTO err = new ErrorDTO() {Title =  "Error", Message = e.Message};
                return BadRequest(err);
            }
        }
    }
}
