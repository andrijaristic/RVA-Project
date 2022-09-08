using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto.StudentDto;
using Server.Dto.SubjectDto;
using Server.Interfaces.ServiceInterfaces;
using System.Security.Cryptography.X509Certificates;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<DetailedSubjectDTO> subjects = await _subjectService.GetAllSubjectsComplete();
                return Ok(subjects);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                DetailedSubjectDTO detailedSubject = await _subjectService.GetSubjectComplete(id);
                return Ok(detailedSubject);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewSubjectDTO newSubjectDTO)
        {
            try 
            {
                DisplaySubjectDTO newSubject = await _subjectService.CreateSubject(newSubjectDTO);
                return Ok(newSubject);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string responseText = await _subjectService.DeleteSubject(id);
                return Ok(responseText);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
