using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Application.Periods.Commands.Create;
using Schedule.Application.Periods.Commands.Delete;
using Schedule.Application.Periods.Commands.Update;
using Schedule.Application.Periods.Queries.Current;
using Schedule.Application.Periods.Queries.GetAll;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Domain.Enums;
using Schedule.Shared.Authorization;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class PeriodController : BaseController
    {
        public PeriodController(ILogger<PeriodController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        /// <summary>
        /// Gets a paginated list of periods
        /// </summary>
        /// <param name="dto">The request filter</param>
        /// <response code="200">A paginated list of periods</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A paginated list of periods</returns>
        [HttpGet]
        [ScheduleHasPermission(SchedulePermissionType.ReadPeriod)]
        [ProducesResponseType(typeof(PaginatedResponseDto<GetAllPeriodsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllPeriods([FromQuery] GetAllPeriodsRequestDto dto)
        {
            Logger.LogInformation($"{nameof(GetAllPeriods)}: Getting periods...");
            var response = await Mediator.Send(new GetAllPeriodsQuery(dto));

            Logger.LogInformation($"{nameof(GetAllPeriods)}: Got = {response.Records} / {response.TotalRecords}");
            return Ok(response);
        }

        /// <summary>
        /// Creates a new period
        /// </summary>
        /// <param name="dto">The request</param>
        /// <response code="200">The created period</response>
        /// <response code="400">If some of the properties in the request are not valid or if the period already exists</response>
        /// <returns>The created period</returns>
        [HttpPost]
        [ScheduleHasPermission(SchedulePermissionType.CreatePeriod)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllPeriodsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreatePeriod(SavePeriodRequestDto dto)
        {
            Logger.LogInformation($"{nameof(CreatePeriod)}: Creating new period...");
            var response = await Mediator.Send(new CreatePeriodCommand(dto));

            Logger.LogInformation($"{nameof(CreatePeriod)}: PeriodId = {response.Result.Id} was successfully created");
            return Ok(response);
        }

        /// <summary>
        /// Updates a new period
        /// </summary>
        /// <param name="id">The period id</param>
        /// <param name="dto">The request</param>
        /// <response code="200">The updated period</response>
        /// <response code="400">If some of the properties in the request are not valid or if the new period  name already exists</response>
        /// <response code="404">If the period does not exist</response>
        /// <returns>The updated period</returns>
        [HttpPut("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.UpdatePeriod)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllPeriodsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdatePeriod(long id, SavePeriodRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdatePeriod)}: Updating periodId = {id}...");
            var response = await Mediator.Send(new UpdatePeriodCommand(id, dto));

            Logger.LogInformation($"{nameof(UpdatePeriod)}: PeriodId = {id} was successfully updated");
            return Ok(response);
        }

        /// <summary>
        /// Deletes a period
        /// </summary>
        /// <param name="id">The period id</param>
        /// <response code="200">The result of the operation</response>
        /// <response code="404">If the period was not found</response>
        /// <returns>The result of the operation</returns>
        [HttpDelete("{id}")]
        [ScheduleHasPermission(SchedulePermissionType.DeletePeriod)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeletePeriod(long id)
        {
            Logger.LogInformation($"{nameof(DeletePeriod)}: Deleting periodId = {id}...");
            var response = await Mediator.Send(new DeletePeriodCommand(id));

            Logger.LogInformation($"{nameof(UpdatePeriod)}: PeriodId = {id} was successfully deleted");
            return Ok(response);
        }

        /// <summary>
        /// Gets the current active period
        /// </summary>
        /// <response code="200">The active period or null</response>
        /// <returns>The active period or null</returns>
        [HttpGet("Current")]
        [ScheduleHasPermission(SchedulePermissionType.ReadPeriod)]
        [ProducesResponseType(typeof(ApiResponseDto<GetAllPeriodsResponseDto>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCurrentPeriod()
        {
            Logger.LogInformation($"{nameof(DeletePeriod)}: Getting current period...");
            var response = await Mediator.Send(new GetCurrentPeriodQuery());

            Logger.LogInformation($"{nameof(DeletePeriod)}: Got current period");
            return Ok(response);
        }
    }
}
