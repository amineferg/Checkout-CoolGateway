using CoolGateway.Application.Payments.Commands;
using CoolGateway.Application.Payments.Queries;
using CoolGateway.WebApi.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoolGateway.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IMediator mediator, ILogger<PaymentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets the payment information that corresponds to the given payment identifier.
    /// </summary>
    /// <param name="paymentId">Payment identifier.</param>
    /// <returns>Payment information if found.</returns>
    [HttpGet("{paymentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPayment(Guid paymentId)
    {
        var paymentQueryDto = await _mediator.Send(new GetPaymentQuery
        {
            PaymentId = paymentId
        });

        return paymentQueryDto == null
            ? NotFound()
            : Ok(paymentQueryDto);
    }

    /// <summary>
    /// Processes the given payment information.
    /// </summary>
    /// <param name="payment">Payment information</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProcessPayment([FromBody]ProcessPaymentDto payment)
    {
        var paymentId = await _mediator.Send(new ProcessPaymentCommand
        {
            MerchantId = payment.MerchantId,
            CardNumber = payment.CardNumber,
            CardHolder = payment.CardHolder,
            CardExpiryMonth = payment.CardExpiryMonth,
            CardExpiryYear = payment.CardExpiryYear,
            CardCvv = payment.CardCvv,
            Amount = payment.Amount,
            Currency = payment.Currency
        });

        return Ok(new ProcessPaymentResultDto { PaymentId = paymentId });
    }
}
