using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Dto;
using Server.Dto.StudentDto;
using Server.Dto.SubjectDto;
using Server.Interfaces.Logger;
using Server.Interfaces.ServiceInterfaces;
using System.Security.Cryptography.X509Certificates;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogging _logger;
        public SubjectsController(ISubjectService subjectService, ILogging logger)
        {
            _subjectService = subjectService;
            _logger = logger;   
        }

        [HttpGet]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting all available subjects", Enums.ELogType.INFO);
                List<DetailedSubjectDTO> subjects = await _subjectService.GetAllSubjectsComplete();
                return Ok(subjects);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Error", Message  = e.Message};
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Getting detailed subject with ID {id}", Enums.ELogType.INFO);
                DetailedSubjectDTO detailedSubject = await _subjectService.GetSubjectComplete(id);
                return Ok(detailedSubject);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Non-existant subject", Message = e.Message};
                return BadRequest(error);
            }
        }

        [HttpPost]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Post([FromBody]NewSubjectDTO newSubjectDTO)
        {
            try 
            {
                _logger.LogMessage($"{User.Identity.Name}: Creating new subject", Enums.ELogType.INFO);
                DisplaySubjectDTO newSubject = await _subjectService.CreateSubject(newSubjectDTO);
                return Ok(newSubject);
            }
            catch (Exception e) 
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Subject creation error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpDelete("delete-subject/{id}")]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Deleting subject with ID {id}", Enums.ELogType.INFO);
                SuccessDTO response = await _subjectService.DeleteSubject(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Subject deletion error", Message = e.Message };
                return BadRequest(error);
            }
        }

        [HttpPut("update-subject")]
        [Authorize(Roles = "admin")]
        [Authorize(Policy = "SystemUser")]
        public async Task<IActionResult> Put([FromBody]SubjectUpdateDTO dto)
        {
            try
            {
                _logger.LogMessage($"{User.Identity.Name}: Updating subject with ID {dto.Id}", Enums.ELogType.INFO);
                SuccessDTO response = await _subjectService.UpdateSubject(dto);
                return Ok(response);
            } catch (Exception e)
            {
                _logger.LogMessage($"{User.Identity.Name}: {e.Message}", Enums.ELogType.ERROR);
                ErrorDTO error = new ErrorDTO() { Title = "Subject update error", Message = e.Message};
                return BadRequest(error);
            }
        }
    }
}
