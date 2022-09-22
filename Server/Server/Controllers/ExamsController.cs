using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.ExamDto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly ILogging _logger;
        public ExamsController(IExamService examService, ILogging logger)
        {
            _examService = examService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting detailed exam with ID {id}", Enums.ELogType.INFO);
                DetailedExamDTO detailedExam = await _examService.GetById(id);
                return Ok(detailedExam);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() {Title = "Non-existant exam", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting all exams", Enums.ELogType.INFO);
                List<DetailedExamDTO> exams = await _examService.GetAllExams();
                return Ok(exams);
            }
            catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody]NewExamDTO newExamDTO)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Creating new exam", Enums.ELogType.INFO);
                DisplayExamDTO examDTO = await _examService.CreateExam(newExamDTO);
                return Ok(examDTO);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Error", Message = e.Message};
                return BadRequest(error);
            }
        }

        [HttpDelete("delete-exam/{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Deleting exam with ID {id}", Enums.ELogType.INFO);
                SuccessDTO response = await _examService.DeleteExam(id);
                return Ok(response);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Exam deletion error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpPut("update-exam")]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody]UpdateExamDTO dto)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Updating exam with ID {dto.Id}", Enums.ELogType.INFO);
                SuccessDTO response = await _examService.UpdateExam(dto);
                return Ok(response);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Exam updating error", Message = e.Message };
                return BadRequest(error);
            }
        }
    }
}
