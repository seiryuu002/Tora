using Microsoft.Extensions.DependencyInjection;
using Tora.Application.Interfaces;
using Tora.Infrastructure.Services;

namespace Tora.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}
