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
    }
}
