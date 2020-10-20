using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Web.Interfaces.Apis;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class PeriodController : BaseController<PeriodController>
    {
        private readonly IPeriodApiService _periodApiService;
        public PeriodController(
            ILogger<PeriodController> logger,
            IPeriodApiService periodApiService)
            : base(logger)
        {
            _periodApiService = periodApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeriods([FromQuery] GetAllPeriodsRequestDto dto)
        {
            var response = await _periodApiService.GetAllPeriods(dto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePeriod(SavePeriodRequestDto dto)
        {
            var response = await _periodApiService.CreatePeriod(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePeriod(long id, SavePeriodRequestDto dto)
        {
            var response = await _periodApiService.UpdatePeriod(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriod(long id)
        {
            var response = await _periodApiService.DeletePeriod(id);
            return Ok(response);
        }

        [HttpGet("Current")]
        public async Task<IActionResult> GetCurrentPeriod()
        {
            var response = await _periodApiService.GetCurrentPeriod();
            return Ok(response);
        }
    }
}
