using CoolGateway.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CoolGateway.Infrastructure.Persistence;

internal class CoolGatewayDbContext : DbContext
{
    public CoolGatewayDbContext(DbContextOptions<CoolGatewayDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentDto> Payments { get; set; }
}
