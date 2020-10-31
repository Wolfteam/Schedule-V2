using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Subjects.Commands.Create;
using Schedule.Application.Subjects.Commands.Delete;
using Schedule.Application.Subjects.Commands.Update;
using Schedule.Application.Subjects.Queries.Get;
using Schedule.Application.Subjects.Queries.GetAll;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Enums;
using Schedule.Shared.Authorization;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class SubjectController : BaseController
    {
        public SubjectController(ILogger<SubjectController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        /// <summary>
        /// Gets a paginated list of subjects
        /// </summary>
        /// <param name="dto">The request filter</param>
        /// <response code="200">A paginated list of subjects</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A paginated list of subjects</returns>
        [HttpGet]
        [ScheduleHasPermission(SchedulePermissionType.ReadSubject)]
        [ProducesResponseType(typeof(PaginatedResponseDto<GetAllSubjectsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllSubjects([FromQuery] GetAllSubjectsRequestDto dto)
        {
            Logger.LogInformation($"{nameof(GetAllSubjects)}: Getting subjects...");
            var response = await Mediator.Send(new GetAllSubjectsQuery(dto));

            Logger.LogInformation($"{nameof(GetAllSubjects)}: Got {response.Records} / {response.TotalRecords}");
            return Ok(response);
        }

        /// <summary>
        /// Gets a particular subject
        /// </summary>
        /// <param name="id">The subject id</param>
        /// <response code="200">The subject</response>
        /// <response code="404">If no  subject was found</response>
        /// <returns>The subject</returns>
        [HttpGet("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.ReadSubject)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllSubjectsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetSubject(long id)
        {
            Logger.LogInformation($"{nameof(GetSubject)}: Getting subjectId = {id}...");
            var response = await Mediator.Send(new GetSubjectQuery(id));

            Logger.LogInformation($"{nameof(GetSubject)}: Got subjectId = {id}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new subject
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created subject</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the classroom, semester or classroom type does not exist</response>
        /// <returns>The created subject</returns>
        [HttpPost]
        [ScheduleHasPermission(SchedulePermissionType.CreateSubject)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllSubjectsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateSubject(SaveSubjectRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateSubject)}: Creating subject...");
            var response = await Mediator.Send(new CreateSubjectCommand(dto));

            Logger.LogInformation($"{nameof(CreateSubject)}: SubjectId = {response.Result.Id} was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updated a subject
        /// </summary>
        /// <param name="id">The subject id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated subject</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the subject, classroom, semester or classroom type does not exist</response>
        /// <returns>The updated subject</returns>
        [HttpPut("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.UpdateSubject)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllSubjectsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateSubject(long id, SaveSubjectRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdateSubject)}: Updating subjectId = {id}...");
            var response = await Mediator.Send(new UpdateSubjectCommand(id, dto));

            Logger.LogInformation($"{nameof(UpdateSubject)}: SubjectId = {id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a subject
        /// </summary>
        /// <param name="id">The subject id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the subject was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.DeleteSubject)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            Logger.LogInformation($"{nameof(UpdateSubject)}: Deleting subjectId = {id}...");
            var response = await Mediator.Send(new DeleteSubjectCommand(id));

            Logger.LogInformation($"{nameof(UpdateSubject)}: SubjectId = {id} was successfully deleted");
            return Ok(response);
        }
    }
}
