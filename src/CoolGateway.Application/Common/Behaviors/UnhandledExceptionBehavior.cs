using CoolGateway.Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoolGateway.Application.Common.Behaviors;

internal class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"# Unhandled exception {e.GetType().Name} => {e.Message}. Request : {@request}");
            throw;
        }
    }
}
