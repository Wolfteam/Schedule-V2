using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Web.Interfaces.Apis;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class ClassroomController : BaseController<ClassroomController>
    {
        private readonly IClassroomApiService _classroomApiService;
        public ClassroomController(
            ILogger<ClassroomController> logger,
            IClassroomApiService classroomApiService)
            : base(logger)
        {
            _classroomApiService = classroomApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassrooms([FromQuery] GetAllClassroomsRequestDto dto)
        {
            var response = await _classroomApiService.GetAllClassrooms(dto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassroom(long id)
        {
            var response = await _classroomApiService.GetClassroom(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassroom(SaveClassroomRequestDto dto)
        {
            var response = await _classroomApiService.CreateClassroom(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassroom(long id, SaveClassroomRequestDto dto)
        {
            var response = await _classroomApiService.UpdateClassroom(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(long id)
        {
            var response = await _classroomApiService.DeleteClassroom(id);
            return Ok(response);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetAllClassroomTypes([FromQuery] GetAllClassroomTypesRequestDto dto)
        {
            var response = await _classroomApiService.GetAllClassroomTypes(dto);
            return Ok(response);
        }

        [HttpGet("Types/{id}")]
        public async Task<IActionResult> GetClassroomType(long id)
        {
            var response = await _classroomApiService.GetClassroomType(id);
            return Ok(response);
        }

        [HttpPost("Types")]
        public async Task<IActionResult> CreateClassroomType(SaveClassroomTypeRequestDto dto)
        {
            var response = await _classroomApiService.CreateClassroomType(dto);
            return Ok(response);
        }

        [HttpPut("Types/{id}")]
        public async Task<IActionResult> UpdateClassroomType(long id, SaveClassroomTypeRequestDto dto)
        {
            var response = await _classroomApiService.UpdateClassroomType(id, dto);
            return Ok(response);
        }

        [HttpDelete("Types/{id}")]
        public async Task<IActionResult> DeleteClassroomType(long id)
        {
            var response = await _classroomApiService.DeleteClassroomType(id);
            return Ok(response);
        }
    }
}
