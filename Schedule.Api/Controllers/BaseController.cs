using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Schedule.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly IMediator Mediator;

        protected BaseController(ILogger logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }
    }
}
