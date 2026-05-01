using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tora.Application.Interfaces;
using Tora.Application.Interfaces.Repositories;
using Tora.Infrastructure.Persistence;
using Tora.Infrastructure.Persistence.Repositories;
using Tora.Infrastructure.Persistence.Seed;
using Tora.Infrastructure.Services;

namespace Tora.Infrastructure;

public static class DependencyInjection
{
    public static void AddToraDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?? 
                               throw new InvalidOperationException("No 'DefaultConnection' found in configuration");
        
        builder.Services.AddDbContext<ToraDbContext>(Options => 
            Options.UseNpgsql(connectionString, npgsql => npgsql.MigrationsAssembly("Tora.Infrastructure")));
        
        builder.Services.AddScoped<IToraDbContext>(provider => provider.GetRequiredService<ToraDbContext>());
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<IUserReadService, UserReadService>();
        services.AddScoped<DbSeeder>();
        services.AddHostedService<DbSeederHostedService>();
        return services;
    }
}
