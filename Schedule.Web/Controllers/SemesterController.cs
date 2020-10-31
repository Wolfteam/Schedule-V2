using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Web.Interfaces.Apis;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class SemesterController : BaseController<SemesterController>
    {
        private readonly ISemesterApiService _semesterApiService;
        public SemesterController(ILogger<SemesterController> logger, ISemesterApiService semesterApiService)
            : base(logger)
        {
            _semesterApiService = semesterApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSemesters()
        {
            var response = await _semesterApiService.GetAllSemesters();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSemester(long id)
        {
            var response = await _semesterApiService.GetSemester(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSemester(SaveSemesterRequestDto dto)
        {
            var response = await _semesterApiService.CreateSemester(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSemester(long id, SaveSemesterRequestDto dto)
        {
            var response = await _semesterApiService.UpdateSemester(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSemester(long id)
        {
            var response = await _semesterApiService.DeleteSemester(id);
            return Ok(response);
        }
    }
}
