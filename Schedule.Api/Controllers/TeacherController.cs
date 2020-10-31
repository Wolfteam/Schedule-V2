using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Priorities.Commands.Create;
using Schedule.Application.Priorities.Commands.Delete;
using Schedule.Application.Priorities.Commands.Update;
using Schedule.Application.Priorities.Queries.Get;
using Schedule.Application.Priorities.Queries.GetAll;
using Schedule.Application.Teachers.Commands.Create;
using Schedule.Application.Teachers.Commands.Delete;
using Schedule.Application.Teachers.Commands.SaveAvailability;
using Schedule.Application.Teachers.Commands.Update;
using Schedule.Application.Teachers.Queries.Get;
using Schedule.Application.Teachers.Queries.GetAll;
using Schedule.Application.Teachers.Queries.GetAvailability;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Enums;
using Schedule.Shared.Authorization;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class TeacherController : BaseController
    {
        public TeacherController(ILogger<TeacherController> logger, IMediator mediator)
            : base(logger, mediator)
        {
        }

        #region Teacher
        /// <summary>
        /// Gets a paginated list of teachers
        /// </summary>
        /// <response code="200">A paginated list of teachers</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A paginated list of teachers</returns>
        [HttpGet]
        [ScheduleHasPermission(SchedulePermissionType.ReadTeacher)]
        [ProducesResponseType(typeof(ApiListResponseDto<GetAllTeacherResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllTeachers()
        {
            Logger.LogInformation($"{nameof(GetAllTeachers)}: Getting teachers...");
            var response = await Mediator.Send(new GetAllTeachersQuery());

            Logger.LogInformation($"{nameof(GetAllTeachers)}: Got {response.Result.Count} teachers");
            return Ok(response);
        }

        /// <summary>
        /// Gets a particular teacher
        /// </summary>
        /// <param name="id">The teacher id</param>
        /// <response code="200">The teacher</response>
        /// <response code="404">If no  teacher was found</response>
        /// <returns>The teacher</returns>
        [HttpGet("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.ReadTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllTeacherResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTeacher(long id)
        {
            Logger.LogInformation($"{nameof(GetTeacher)}: Getting teacherId = {id}...");
            var response = await Mediator.Send(new GetTeacherQuery(id));

            Logger.LogInformation($"{nameof(GetTeacher)}: Got teacherId = {id}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new teacher
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created teacher</response>
        /// <response code="400">If some of the properties in the request are not valid or if the identifier number is being used</response>
        /// <response code="404">If the priority does not exist</response>
        /// <returns>The created teacher</returns>
        [HttpPost]
        [ScheduleHasPermission(SchedulePermissionType.CreateTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllTeacherResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateTeacher(SaveTeacherRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateTeacher)}: Creating a new teacher...");
            var response = await Mediator.Send(new CreateTeacherCommand(dto));

            Logger.LogInformation($"{nameof(CreateTeacher)}: Teacher was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a teacher
        /// </summary>
        /// <param name="id">The teacher id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated teacher</response>
        /// <response code="400">If some of the properties in the request are not valid or if the identifier number is being used</response>
        /// <response code="404">If the priority does not exist</response>
        /// <returns>The updated teacher</returns>
        [HttpPut("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.UpdateTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllTeacherResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateTeacher(long id, SaveTeacherRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateTeacher)}: Updating teacherId = {id}");
            var response = await Mediator.Send(new UpdateTeacherCommand(id, dto));

            Logger.LogInformation($"{nameof(CreateTeacher)}: Teacher was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a teacher
        /// </summary>
        /// <param name="id">The subject id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the teacher was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.DeleteTeacher)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteTeacher(long id)
        {
            Logger.LogInformation($"{nameof(DeleteTeacher)}: Deleting teacherId = {id}");
            var response = await Mediator.Send(new DeleteTeacherCommand(id));

            Logger.LogInformation($"{nameof(DeleteTeacher)}: Teacher was successfully deleted");
            return Ok(response);
        }
        #endregion

        #region Availability
        /// <summary>
        /// Gets the availability for a particular teacher
        /// </summary>
        /// <param name="id">The teacher id</param>
        /// <response code="200">The availability for a particular teacher</response>
        /// <returns>The availability for a particular teacher</returns>
        [HttpGet("{id}/Availability")]
        [ScheduleHasPermission(SchedulePermissionType.ReadTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<TeacherAvailabilityDetailsResponseDto>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAvailability(long id)
        {
            Logger.LogInformation($"{nameof(GetAvailability)}: Getting availability for teacherId = {id}");
            var response = await Mediator.Send(new GetTeacherAvailabilityQuery(id));

            Logger.LogInformation($"{nameof(GetAvailability)}: Got availability for teacherId = {id}");
            return Ok(response);
        }

        /// <summary>
        /// Saves the provided availability to a particular teacher
        /// </summary>
        /// <param name="id">The teacher id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The saved availability</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the teacher or the period does not exist</response>
        /// <returns>The saved availability</returns>
        [HttpPost("{id}/Availability")]
        [ScheduleHasPermission(SchedulePermissionType.CreateTeacher)]
        [ProducesResponseType(typeof(ApiListResponseDto<TeacherAvailabilityResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SaveAvailability(long id, SaveTeacherAvailabilityRequestDto dto)
        {
            Logger.LogInformation($"{nameof(SaveAvailability)}: Saving availability for teacherId = {id}");
            var response = await Mediator.Send(new SaveAvailabilityCommand(id, dto));

            Logger.LogInformation($"{nameof(SaveAvailability)}: Availability for teacherId = {id} was successfully saved");
            return Ok(response);
        }
        #endregion

        #region Priority
        /// <summary>
        /// Gets a paginated list of priority
        /// </summary>
        /// <response code="200">A paginated list of priority</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A paginated list of priority</returns>
        [HttpGet("Priorities")]
        [ScheduleHasPermission(SchedulePermissionType.ReadTeacher)]
        [ProducesResponseType(typeof(ApiListResponseDto<GetAllPrioritiesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllPriorities()
        {
            Logger.LogInformation($"{nameof(GetAllPriorities)}: Getting all priorities...");
            var response = await Mediator.Send(new GetAllPrioritiesQuery());

            Logger.LogInformation($"{nameof(GetAllPriorities)}: Got {response.Result.Count} priorities");
            return Ok(response);
        }

        /// <summary>
        /// Gets a particular priority
        /// </summary>
        /// <param name="id">The priority id</param>
        /// <response code="200">The priority</response>
        /// <response code="404">If no  priority was found</response>
        /// <returns>The priority</returns>
        [HttpGet("Priorities/{id}")]
        [ScheduleHasPermission(SchedulePermissionType.ReadTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllPrioritiesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetPriority(long id)
        {
            Logger.LogInformation($"{nameof(GetPriority)}: Getting priorityId = {id}...");
            var response = await Mediator.Send(new GetPriorityQuery(id));

            Logger.LogInformation($"{nameof(GetPriority)}: Got priorityId = {id}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new priority
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created priority</response>
        /// <response code="400">If some of the properties in the request are not valid or if priority name is being used</response>
        /// <returns>The created priority</returns>
        [HttpPost("Priorities")]
        [ScheduleHasPermission(SchedulePermissionType.CreateTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllPrioritiesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreatePriority(SavePriorityRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreatePriority)}: Creating a new priority...");
            var response = await Mediator.Send(new CreatePriorityCommand(dto));

            Logger.LogInformation($"{nameof(CreatePriority)}: PriorityId = {response.Result.Id} was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a priority
        /// </summary>
        /// <param name="id">The priority id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated priority</response>
        /// <response code="400">If some of the properties in the request are not valid or if priority name is being used</response>
        /// <response code="404">If the priority does not exist</response>
        /// <returns>The updated priority</returns>
        [HttpPut("Priorities/{id}")]
        [ScheduleHasPermission(SchedulePermissionType.UpdateTeacher)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllPrioritiesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdatePriority(long id, SavePriorityRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdatePriority)}: Updating priorityId = {id}...");
            var response = await Mediator.Send(new UpdatePriorityCommand(id, dto));

            Logger.LogInformation($"{nameof(UpdatePriority)}: PriorityId = {id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a priority
        /// </summary>
        /// <param name="id">The priority id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the priority was not found</response>
        /// <returns>The result of the operation</returns>
        [ScheduleHasPermission(SchedulePermissionType.DeleteTeacher)]
        [HttpDelete("Priorities/{id}")]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeletePriority(long id)
        {
            Logger.LogInformation($"{nameof(DeletePriority)}: Deleting priorityId = {id}...");
            var response = await Mediator.Send(new DeletePriorityCommand(id));

            Logger.LogInformation($"{nameof(DeletePriority)}: PriorityId = {id} was successfully deleted");
            return Ok(response);
        }
        #endregion
    }
}
