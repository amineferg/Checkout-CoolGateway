using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CoolGateway.Application.Common.Behaviors;

internal class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private const int LongRequestThresholdInMilliseconds = 500;
    private readonly Stopwatch _timer;
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
        _timer = new Stopwatch();
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > LongRequestThresholdInMilliseconds)
        {
            _logger.LogWarning($"# Long request => {typeof(TRequest).Name} took {_timer.ElapsedMilliseconds} milliseconds to complete the request {@request}.");
        }

        return response;
    }
}
