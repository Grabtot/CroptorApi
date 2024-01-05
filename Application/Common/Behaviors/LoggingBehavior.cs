using Croptor.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Croptor.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IUserProvider _userProvider;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IUserProvider userProvider)
        {
            _logger = logger;
            _userProvider = userProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string name = _userProvider.UserName != null ?
                $"by {_userProvider.UserName} ({_userProvider.UserId})" :
                string.Empty;

            _logger.LogDebug($"Executing request: {typeof(TRequest).Name} {name}");

            TResponse? response = await next();

            _logger.LogDebug($"Request {typeof(TRequest).Name} completed {name}");

            return response;
        }
    }
}
