using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Tora.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>( 
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger) : 
    IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
{
    private const int slowRequestThresholdMs = 500;
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await next(cancellationToken);
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;

        if(elapsed >= slowRequestThresholdMs)
        {
            logger.LogWarning("[Performance] SLOW REQUEST: {requestName} took {elapsedMs}ms (threshold: {threshold}ms). Request: {@Request}", typeof(TRequest).Name, elapsed, slowRequestThresholdMs, request);
        }

        return response;
    }
}
