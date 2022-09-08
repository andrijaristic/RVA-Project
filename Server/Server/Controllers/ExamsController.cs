using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto.ExamDto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;
using Server.Interfaces.ServiceInterfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                DetailedExamDTO detailedExam = await _examService.GetById(id);
                return Ok(detailedExam);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<DetailedExamDTO> exams = await _examService.GetAllExams();
                return Ok(exams);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody]NewExamDTO newExamDTO)
        {
            try
            {
                DisplayExamDTO examDTO = await _examService.CreateExam(newExamDTO);
                return Ok(examDTO);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _examService.DeleteExam(id);
                return Ok("Successfully removed exam!");
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
