using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Semesters.Commands.Create;
using Schedule.Application.Semesters.Commands.Delete;
using Schedule.Application.Semesters.Commands.Update;
using Schedule.Application.Semesters.Queries.GetAll;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Domain.Enums;
using Schedule.Shared.Authorization;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class SemesterController : BaseController
    {
        public SemesterController(
            ILogger<SemesterController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
        }

        /// <summary>
        /// Gets a list of semesters
        /// </summary>
        /// <response code="200">A list of semesters</response>
        /// <returns>A list of semesters</returns>
        [HttpGet]
        [ScheduleHasPermission(SchedulePermissionType.ReadSemester)]
        [ProducesResponseType(typeof(ApiListResponseDto<GetAllSemestersResponseDto>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllSemesters()
        {
            Logger.LogInformation($"{nameof(GetAllSemesters)}: Getting all semesters...");
            var response = await Mediator.Send(new GetAllSemestersQuery());

            Logger.LogInformation($"{nameof(GetAllSemesters)}: Got = {response.Result.Count} semesters");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new semester
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created semester</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the school does not exists</response>
        /// <returns>The created semester</returns>
        [HttpPost]
        [ScheduleHasPermission(SchedulePermissionType.CreateSemester)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllSemestersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateSemester(SaveSemesterRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateSemester)}: Creating a new semester...");
            var response = await Mediator.Send(new CreateSemesterCommand(dto));

            Logger.LogInformation($"{nameof(CreateSemester)}: SemesterId = {response.Result.Id} was created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a  semester
        /// </summary>
        /// <param name="id">The semester id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated semester</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the semester was not found</response>
        /// <returns>The updated semester</returns>
        [HttpPut("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.UpdateSemester)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllSemestersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateSemester(long id, SaveSemesterRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateSemester)}: Updating semesterId = {id}...");
            var response = await Mediator.Send(new UpdateSemesterCommand(id, dto));

            Logger.LogInformation($"{nameof(CreateSemester)}: SemesterId = {id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a semester
        /// </summary>
        /// <param name="id">The semester id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the semester was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.DeleteSemester)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteSemester(long id)
        {
            Logger.LogInformation($"{nameof(CreateSemester)}: Deleting semesterId = {id}...");
            var response = await Mediator.Send(new DeleteSemesterCommand(id));

            Logger.LogInformation($"{nameof(CreateSemester)}: SemesterId = {id} was successfully deleted");
            return Ok(response);
        }
    }
}
