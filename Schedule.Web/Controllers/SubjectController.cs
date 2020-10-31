using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Web.Interfaces.Apis;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class SubjectController : BaseController<SubjectController>
    {
        private readonly ISubjectApiService _subjectApiService;
        public SubjectController(
            ILogger<SubjectController> logger,
            ISubjectApiService subjectApiService)
            : base(logger)
        {
            _subjectApiService = subjectApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects([FromQuery] GetAllSubjectsRequestDto dto)
        {
            var response = await _subjectApiService.GetAllSubjects(dto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(long id)
        {
            var response = await _subjectApiService.GetSubject(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(SaveSubjectRequestDto dto)
        {
            var response = await _subjectApiService.CreateSubject(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(long id, SaveSubjectRequestDto dto)
        {
            var response = await _subjectApiService.UpdateSubject(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            var response = await _subjectApiService.DeleteSubject(id);
            return Ok(response);
        }
    }
}
