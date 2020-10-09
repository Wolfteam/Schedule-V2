using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Subjects.Queries.GetAllTeachers;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class TeacherController : BaseController
    {
        public TeacherController(ILogger<TeacherController> logger, IMediator mediator)
            : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTeachersQuery query)
        {
            Logger.LogInformation($"{nameof(GetAll)}: Getting teachers...");
            var response = await Mediator.Send(query);

            Logger.LogInformation($"{nameof(GetAll)}: Got {response.Records} / {response.TotalRecords} teachers");
            return Ok(response);
        }
    }
}
