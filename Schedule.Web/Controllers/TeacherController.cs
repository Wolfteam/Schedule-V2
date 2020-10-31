using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Web.Interfaces.Apis;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class TeacherController : BaseController<TeacherController>
    {
        private readonly ITeacherApiService _teacherApiService;
        public TeacherController(
            ILogger<TeacherController> logger,
            ITeacherApiService teacherApiService)
            : base(logger)
        {
            _teacherApiService = teacherApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var response = await _teacherApiService.GetAllTeachers();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(long id)
        {
            var response = await _teacherApiService.GetTeacher(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(SaveTeacherRequestDto dto)
        {
            var response = await _teacherApiService.CreateTeacher(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(long id, SaveTeacherRequestDto dto)
        {
            var response = await _teacherApiService.UpdateTeacher(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(long id)
        {
            var response = await _teacherApiService.DeleteTeacher(id);
            return Ok(response);
        }

        [HttpGet("{id}/Availability")]
        public async Task<IActionResult> GetAvailability(long id)
        {
            var response = await _teacherApiService.GetAvailability(id);
            return Ok(response);
        }

        [HttpPost("{id}/Availability")]
        public async Task<IActionResult> SaveAvailability(long id, SaveTeacherAvailabilityRequestDto dto)
        {
            var response = await _teacherApiService.SaveAvailability(id, dto);
            return Ok(response);
        }

        [HttpGet("Priorities")]
        public async Task<IActionResult> GetAllPriorities()
        {
            var response = await _teacherApiService.GetAllPriorities();
            return Ok(response);
        }

        [HttpGet("Priorities/{id}")]
        public async Task<IActionResult> GetPriority(long id)
        {
            var response = await _teacherApiService.GetPriority(id);
            return Ok(response);
        }

        [HttpPost("Priorities")]
        public async Task<IActionResult> CreatePriority(SavePriorityRequestDto dto)
        {
            var response = await _teacherApiService.CreatePriority(dto);
            return Ok(response);
        }

        [HttpPut("Priorities/{id}")]
        public async Task<IActionResult> UpdatePriority(long id, SavePriorityRequestDto dto)
        {
            var response = await _teacherApiService.UpdatePriority(id, dto);
            return Ok(response);
        }

        [HttpDelete("Priorities/{id}")]
        public async Task<IActionResult> DeletePriority(long id)
        {
            var response = await _teacherApiService.DeletePriority(id);
            return Ok(response);
        }
    }
}
