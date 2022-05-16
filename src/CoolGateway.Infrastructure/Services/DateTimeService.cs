using CoolGateway.Application.Common;

namespace CoolGateway.Infrastructure.Services;

internal class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
