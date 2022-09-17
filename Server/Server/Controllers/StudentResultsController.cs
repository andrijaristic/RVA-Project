using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.ExamDto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentResultsController : ControllerBase
    {
        private readonly IStudentResultService _studentResultService;

        public StudentResultsController(IStudentResultService studentResultService)
        {
            _studentResultService = studentResultService;
        }

        [HttpGet("get-students/{id}")]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> ListStudents(int id)
        {
            try
            {
                List<DisplayStudentResultDTO> students = await _studentResultService.GetAllStudentsForExam(id);
                return Ok(students);
            }
            catch (Exception e)
            {
                ErrorDTO error = new ErrorDTO() { Title = "Error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpGet("get-exams/{id}")]
        public async Task<IActionResult> GetStudentExams(int id)
        {
            try
            {
                List<StudentExamsDTO> registedExams = await _studentResultService.GetExamsForStudent(id);
                return Ok(registedExams);
            } catch (Exception e)
            {
                ErrorDTO error = new ErrorDTO() { Title = "Error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpPost("apply")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentResultDTO addStudentResultDTO)
        {
            try
            {
                DisplayStudentDTO studentDTO = await _studentResultService.AddStudentToExam(addStudentResultDTO);
                return Ok(studentDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("remove")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> RemoveStudent([FromBody] AddStudentResultDTO addStudentDTO)
        {
            try
            {
                string studentDTO = await _studentResultService.RemoveStudentFromExam(addStudentDTO);
                return Ok(studentDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
