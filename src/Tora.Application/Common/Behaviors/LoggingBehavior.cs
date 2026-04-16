using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Tora.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
             ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
             IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("[MediatR] handling {requestName} ", requestName);
        var stopWatch =  Stopwatch.StartNew();
        TResponse response;

        try
        {
            response = await next(cancellationToken);
        }
        catch(Exception ex)
        {
            stopWatch.Stop();
            logger.LogError(ex, "[MediatR] {requestName} failed after {elapsedMs}ms", requestName, stopWatch.ElapsedMilliseconds);
            throw;
        }

        stopWatch.Stop();
        logger.LogInformation("[MediatR] {requestName} completed in {ElapsedMs}ms", requestName, stopWatch.ElapsedMilliseconds);

        return response;
    }
}
