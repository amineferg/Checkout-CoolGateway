using CoolGateway.Application.Common.Exceptions;
using CoolGateway.Domain.Common;
using CoolGateway.Domain.Payments.Exceptions;
using CoolGateway.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoolGateway.WebApi.Filters;

/// <summary>
/// Check @ https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling .
/// </summary>
public class GatewayExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public GatewayExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ApplicationQueryException), HandleApplicationQueryException },
            { typeof(ApplicationPaymentRejectedException), HandleApplicationPaymentRejectedException },
            { typeof(ApplicationValidationException), HandleApplicationValidationException },
            { typeof(PaymentDomainException), HandlePaymentDomainException },
            { typeof(PaymentDomainValidationException), HandlePaymentDomainValidationException },
            { typeof(CoolGatewayApplicationException), HandleCoolGatewayApplicationException },
            { typeof(CoolGatewayDomainException), HandleCoolGatewayDomainException },
            { typeof(CoolGatewayException), HandleCoolGatewayException },
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleCoolGatewayApplicationException(ExceptionContext context)
    {
        var exception = (CoolGatewayApplicationException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleCoolGatewayDomainException(ExceptionContext context)
    {
        var exception = (CoolGatewayDomainException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleApplicationQueryException(ExceptionContext context)
    {
        var exception = (ApplicationQueryException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }


    private void HandleApplicationPaymentRejectedException(ExceptionContext context)
    {
        var exception = (ApplicationPaymentRejectedException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleApplicationValidationException(ExceptionContext context)
    {
        var exception = (ApplicationValidationException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandlePaymentDomainException(ExceptionContext context)
    {
        var exception = (PaymentDomainException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandlePaymentDomainValidationException(ExceptionContext context)
    {
        var exception = (PaymentDomainValidationException)context.Exception;

        var details = new ProblemDetails()
        {
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleCoolGatewayException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing the request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing the request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
