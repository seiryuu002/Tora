using Microsoft.Extensions.DependencyInjection;
using Tora.Infrastructure.Persistence.Seed;
using Tora.Infrastructure.Services;
using Tora.Infrastructure.Interfaces;

namespace Tora.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<DbSeeder>();
        services.AddHostedService<DbSeederHostedService>();
        return services;
    }
}
