using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Interfaces.Managers;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IDefaultAppUserManager _userManager;

        public LoggingBehaviour(ILogger<TRequest> logger, IDefaultAppUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("Handling request: {Name} for {@UserName} {@Request}", requestName, _userManager.Username, request);
            return Task.CompletedTask;
        }
    }
}
