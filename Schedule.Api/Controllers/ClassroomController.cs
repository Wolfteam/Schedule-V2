using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Classrooms.Commands.Create;
using Schedule.Application.Classrooms.Commands.CreateType;
using Schedule.Application.Classrooms.Commands.Delete;
using Schedule.Application.Classrooms.Commands.DeleteType;
using Schedule.Application.Classrooms.Commands.Update;
using Schedule.Application.Classrooms.Commands.UpdateType;
using Schedule.Application.Classrooms.Queries.GetAll;
using Schedule.Application.Classrooms.Queries.GetAllTypes;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class ClassroomController : BaseController
    {
        public ClassroomController(ILogger<ClassroomController> logger, IMediator mediator)
            : base(logger, mediator)
        {
        }

        #region Classroom
        /// <summary>
        /// Gets a paginated list of classrooms
        /// </summary>
        /// <param name="dto">The request filter</param>
        /// <response code="200">A paginated list of classrooms</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A paginated list of classrooms</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponseDto<GetAllClassroomsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllClassrooms([FromQuery] GetAllClassroomsRequestDto dto)
        {
            Logger.LogInformation($"{nameof(GetAllClassrooms)}: Getting classrooms...");
            var response = await Mediator.Send(new GetAllClassroomsQuery(dto));

            Logger.LogInformation($"{nameof(GetAllClassrooms)}: Got = {response.Records} / {response.TotalRecords}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new classroom
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created classroom</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the classroom type was not found</response>
        /// <returns>The created classroom</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllClassroomsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateClassroom(SaveClassroomRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateClassroom)}: Creating a new classroom...");
            var response = await Mediator.Send(new CreateClassroomCommand(dto));

            Logger.LogInformation($"{nameof(CreateClassroom)}: Classroom id = {response.Result.Id} was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a  classroom
        /// </summary>
        /// <param name="id">The classroom id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated classroom</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <response code="404">If the classroom or the classroom type was not found</response>
        /// <returns>The updated classroom</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllClassroomsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateClassroom(long id, SaveClassroomRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdateClassroom)}: Updating classroomId = {id}...");
            var response = await Mediator.Send(new UpdateClassroomCommand(id, dto));

            Logger.LogInformation($"{nameof(UpdateClassroom)}: Classroom id = {id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a classroom
        /// </summary>
        /// <param name="id">The classroom id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the classroom was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteClassroom(long id)
        {
            Logger.LogInformation($"{nameof(DeleteClassroom)}: Deleting classroomId = {id}...");
            var response = await Mediator.Send(new DeleteClassroomCommand(id));

            Logger.LogInformation($"{nameof(DeleteClassroom)}: Classroom id = {id} was successfully deleted");
            return Ok(response);
        }
        #endregion

        #region Classroom Type
        /// <summary>
        /// Gets a paginated list of classroom types
        /// </summary>
        /// <param name="dto">The request filter</param>
        /// <response code="200">A paginated list of classroom types</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A paginated list of classroom types</returns>
        [HttpGet("Types")]
        [ProducesResponseType(typeof(PaginatedResponseDto<GetAllClassroomTypesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllClassroomTypes([FromQuery] GetAllClassroomTypesRequestDto dto)
        {
            Logger.LogInformation($"{nameof(GetAllClassroomTypes)}: Getting classrooms types...");
            var response = await Mediator.Send(new GetAllClassroomTypesQuery(dto));

            Logger.LogInformation($"{nameof(GetAllClassroomTypes)}: Got = {response.Records} / {response.TotalRecords}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new classroom type
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created classroom type</response>
        /// <response code="400">If some of the properties in the request are not valid or if the type already exists</response>
        /// <returns>The created classroom</returns>
        [HttpPost("Types")]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllClassroomTypesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateClassroomType(SaveClassroomTypeRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreateClassroomType)}: Creating a new classroom type...");
            var response = await Mediator.Send(new CreateClassroomTypeCommand(dto));

            Logger.LogInformation($"{nameof(CreateClassroomType)}: ClassroomTypeId = {response.Result.Id} was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a  classroom type
        /// </summary>
        /// <param name="id">The classroom type id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated classroom type</response>
        /// <response code="400">If some of the properties in the request are not valid or if the type already exists</response>
        /// <response code="404">If the classroom type was not found</response>
        /// <returns>The updated classroom</returns>
        [HttpPut("Types/{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllClassroomTypesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateClassroomType(long id, SaveClassroomTypeRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdateClassroomType)}: Updating classroomTypeId = {id}...");
            var response = await Mediator.Send(new UpdateClassroomTypeCommand(id, dto));

            Logger.LogInformation($"{nameof(UpdateClassroomType)}: ClassroomTypeId = {id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a classroom type
        /// </summary>
        /// <param name="id">The classroom type id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the classroom type was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("Types/{id}")]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteClassroomType(long id)
        {
            Logger.LogInformation($"{nameof(DeleteClassroomType)}: Deleting classroomTypeId = {id}...");
            var response = await Mediator.Send(new DeleteClassroomTypeCommand(id));

            Logger.LogInformation($"{nameof(DeleteClassroomType)}: ClassroomTypeId = {id} was successfully deleted");
            return Ok(response);
        }
        #endregion
    }
}
