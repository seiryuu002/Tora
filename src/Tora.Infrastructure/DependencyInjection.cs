using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tora.Application.Interfaces;
using Tora.Infrastructure.Persistence;
using Tora.Infrastructure.Persistence.Seed;
using Tora.Infrastructure.Services;

namespace Tora.Infrastructure;

public static class DependencyInjection
{
    public static void AddToraDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultCOnnection");
        builder.Services.AddDbContext<ToraDbContext>(Options => 
            Options.UseSqlite(connectionString, builder => builder.MigrationsAssembly("Tora.Infrastructure")));
        builder.Services.AddScoped<IToraDbContext>(provider => provider.GetRequiredService<ToraDbContext>());
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<DbSeeder>();
        services.AddHostedService<DbSeederHostedService>();
        return services;
    }
}
