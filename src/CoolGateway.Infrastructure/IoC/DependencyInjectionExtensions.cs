using CoolGateway.Application.Common;
using CoolGateway.Domain.Payments.Repositories;
using CoolGateway.Infrastructure.Bank;
using CoolGateway.Infrastructure.Persistence;
using CoolGateway.Infrastructure.Persistence.Repositories;
using CoolGateway.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoolGateway.Infrastructure.IoC;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<CoolGatewayDbContext>(options =>
                options.UseInMemoryDatabase("CoolGatewayDb"));
        }
        else
        {
            services.AddDbContext<CoolGatewayDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CoolConnectionString"),
                    b => b.MigrationsAssembly(typeof(CoolGatewayDbContext).Assembly.FullName)));
        }

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IBankClient, BankClient>();

        return services;
    }

    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<CoolGatewayDbContext>();
            if (dbContext == null)
            {
                return;
            }

            dbContext.Database.EnsureCreated();

            // seed and save changes if needed.
        }
    }
}
