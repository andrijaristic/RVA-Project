using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.ExamDto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentResultsController : ControllerBase
    {
        private readonly IStudentResultService _studentResultService;
        private readonly ILogging _logger;

        public StudentResultsController(IStudentResultService studentResultService, ILogging logger)
        {
            _studentResultService = studentResultService;
            _logger = logger;
        }

        [HttpGet("get-students/{id}")]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> ListStudents(int id)
        {
            try 
            {
                _logger.LogMessage($"{User.Identity.Name}: Listing all students registered for exam with ID {id}", Enums.ELogType.INFO);
                List<DisplayStudentResultDTO> students = await _studentResultService.GetAllStudentsForExam(id);
                return Ok(students);
            }
            catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpGet("get-exams/{id}")]
        public async Task<IActionResult> GetStudentExams(int id)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting all exams for student with ID {id}", Enums.ELogType.INFO);
                List<StudentExamsDTO> registedExams = await _studentResultService.GetExamsForStudent(id);
                return Ok(registedExams);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
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
                _logger.LogMessage($"{User.Identity.Name}: Adding student with ID {addStudentResultDTO.StudentId} to exam with ID {addStudentResultDTO.ExamId}", Enums.ELogType.INFO);
                DisplayExamDTO examDTO = await _studentResultService.AddStudentToExam(addStudentResultDTO);
                return Ok(examDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Exam registration error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpDelete("remove")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> RemoveStudent([FromBody] AddStudentResultDTO addStudentDTO)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Removing student with ID {addStudentDTO.StudentId} from exam with ID {addStudentDTO.ExamId}", Enums.ELogType.INFO);
                DisplayExamDTO examDTO = await _studentResultService.RemoveStudentFromExam(addStudentDTO);
                return Ok(examDTO);
            }
            catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Exam withdrawal error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpGet("view")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> ViewStudentResult([FromQuery] AddStudentResultDTO studentResultDTO)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Viewing results of exam with ID {studentResultDTO.ExamId} for student with ID {studentResultDTO.StudentId}", Enums.ELogType.INFO);
                DisplayStudentResultDTO response = await _studentResultService.GetResultsForStudent(studentResultDTO);
                return Ok(response);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Result fetch error", Message = e.Message };
                return BadRequest(error);
            }

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody] GradeStudentDTO gradeStudentDTO)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Grading student with ID {gradeStudentDTO.StudentId} for exam with ID {gradeStudentDTO.ExamId}", Enums.ELogType.INFO);
                DisplayStudentResultDTO response = await _studentResultService.GradeStudentExam(gradeStudentDTO);
                return Ok(response);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Error while grading exam", Message = e.Message };
                return BadRequest(error);
            }
        }
    }
}
