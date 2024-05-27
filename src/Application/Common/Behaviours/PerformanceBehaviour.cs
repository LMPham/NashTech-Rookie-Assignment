using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// A pipeline behavior for warning against long running user requests.
    /// </summary>
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly Stopwatch timer;
        private readonly ILogger<TRequest> logger;
        private readonly IUser user;
        private readonly IIdentityService identityService;

        // Threshhold to determine long running requests
        private const int runTimeThreshHold = 500;

        public PerformanceBehaviour(
            ILogger<TRequest> _logger,
            IUser _user,
            IIdentityService _identityService)
        {
            timer = new Stopwatch();

            logger = _logger;
            user = _user;
            identityService = _identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            timer.Start();

            var response = await next();

            timer.Stop();

            var elapsedMilliseconds = timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > runTimeThreshHold)
            {
                var requestName = typeof(TRequest).Name;
                var userId = user.Id ?? string.Empty;
                var userName = string.Empty;

                if (!string.IsNullOrEmpty(userId))
                {
                    userName = await identityService.GetUserNameAsync(userId);
                }

                logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}
