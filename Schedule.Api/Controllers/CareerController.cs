using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Careers.Commands.Create;
using Schedule.Application.Careers.Commands.Delete;
using Schedule.Application.Careers.Commands.Update;
using Schedule.Application.Careers.Queries.Get;
using Schedule.Application.Careers.Queries.GetAll;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Domain.Enums;
using Schedule.Shared.Authorization;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class CareerController : BaseController
    {
        public CareerController(
            ILogger<CareerController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
        }

        /// <summary>
        /// Gets a list of careers
        /// </summary>
        /// <response code="200">A list of careers</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A list of careers</returns>
        [HttpGet]
        [ScheduleHasPermission(SchedulePermissionType.ReadCareer)]
        [ProducesResponseType(typeof(ApiListResponseDto<GetAllCareersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllCareers()
        {
            Logger.LogInformation($"{nameof(GetAllCareers)}: Getting al careers...");
            var response = await Mediator.Send(new GetAllCareersQuery());

            Logger.LogInformation($"{nameof(GetAllCareers)}: Got = {response.Result.Count} careers");
            return Ok(response);
        }

        /// <summary>
        /// Gets a particular career
        /// </summary>
        /// <param name="id">The career id</param>
        /// <response code="200">The career</response>
        /// <response code="404">If no  career was found</response>
        /// <returns>The career</returns>
        [HttpGet("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.ReadCareer)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllCareersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCareer(long id)
        {
            Logger.LogInformation($"{nameof(GetCareer)}: Getting careerId = {id}...");
            var response = await Mediator.Send(new GetCareerQuery(id));

            Logger.LogInformation($"{nameof(GetCareer)}: Got careerId = {id}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new career
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created career</response>
        /// <response code="400">If some of the properties in the request are not valid or if the career already exists</response>
        /// <returns>The created career</returns>
        [HttpPost]
        [ScheduleHasPermission(SchedulePermissionType.CreateCareer)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllCareersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateCareer(SaveCareerRequestDto dto)
        {
            Logger.LogInformation($"{nameof(GetAllCareers)}: Trying to create a new career...");
            var response = await Mediator.Send(new CreateCareerCommand(dto));

            Logger.LogInformation($"{nameof(GetAllCareers)}: CareerId = {response.Result.Id} was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a  career
        /// </summary>
        /// <param name="id">The career id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated career</response>
        /// <response code="400">If some of the properties in the request are not valid or if the career already exists</response>
        /// <response code="404">If the career was not found</response>
        /// <returns>The updated career</returns>
        [HttpPut("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.UpdateCareer)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllCareersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateCareer(long id, SaveCareerRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdateCareer)}: Trying to create a new career...");
            var response = await Mediator.Send(new UpdateCareerCommand(id, dto));

            Logger.LogInformation($"{nameof(UpdateCareer)}: CareerId = {response.Result.Id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a career
        /// </summary>
        /// <param name="id">The career id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the career was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.DeleteCareer)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteCareer(long id)
        {
            Logger.LogInformation($"{nameof(DeleteCareer)}: Trying to create a new career...");
            var response = await Mediator.Send(new DeleteCareerCommand(id));

            Logger.LogInformation($"{nameof(DeleteCareer)}: CareerId = {id} was successfully updated");
            return Ok(response);
        }
    }
}
