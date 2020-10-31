using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Web.Interfaces.Apis;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class CareerController : BaseController<CareerController>
    {
        private readonly ICareerApiService _careerApiService;

        public CareerController(ILogger<CareerController> logger, ICareerApiService careerApiService) : base(logger)
        {
            _careerApiService = careerApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCareers()
        {
            var response = await _careerApiService.GetAllCareers();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCareer(long id)
        {
            var response = await _careerApiService.GetCareer(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCareer(SaveCareerRequestDto dto)
        {
            var response = await _careerApiService.CreateCareer(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCareer(long id, SaveCareerRequestDto dto)
        {
            var response = await _careerApiService.UpdateCareer(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareer(long id)
        {
            var response = await _careerApiService.DeleteCareer(id);
            return Ok(response);
        }
    }
}
