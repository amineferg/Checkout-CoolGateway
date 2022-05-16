using CoolGateway.Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace CoolGateway.Application.Common.Behaviors;

internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(
                validator => validator.ValidateAsync(context, cancellationToken)));

        var validationFailures = validationResults
            .Where(result => result.Errors.Any())
            .SelectMany(result => result.Errors)
            .ToList();

        if (validationFailures.Any())
        {
            var errorsDictionary = validationFailures
               .GroupBy(validationFailure => validationFailure.PropertyName, validationFailure => validationFailure.ErrorMessage)
               .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            throw new ApplicationValidationException(JsonConvert.SerializeObject(errorsDictionary));
        }

        return await next();
    }
}
