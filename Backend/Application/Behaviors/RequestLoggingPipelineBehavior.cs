using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Shared;

namespace Application.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest,TResponse>
    : IPipelineBehavior<TRequest,TResponse>
    where TRequest : class
    where TResponse : IResult
{

    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest,TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken cancellationToken)
    {
        string requestName  = typeof(TRequest).Name;
        _logger.LogInformation("Processing request {RequestName}",requestName);

        TResponse result = await next();

        if(result.IsSuccess)
        {
            _logger.LogInformation("Completed Request {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Errors, true))
            {
                _logger.LogError("Completed Request {RequestName} with errors", requestName);
            }
        }
        return result;
    }
}
