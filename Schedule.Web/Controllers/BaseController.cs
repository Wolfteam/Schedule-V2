using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Schedule.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> Logger;

        protected BaseController(ILogger<TController> logger)
        {
            Logger = logger;
        }
    }
}
