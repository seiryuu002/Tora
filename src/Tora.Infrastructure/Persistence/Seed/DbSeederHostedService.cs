using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tora.Persistence;

namespace Tora.Infrastructure.Persistence.Seed;

public class DbSeederHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken ct)
    {
        using var scope = serviceProvider.CreateScope();

        var context  = scope.ServiceProvider.GetRequiredService<ToraDbContext>();
        var seeder =  scope.ServiceProvider.GetRequiredService<DbSeeder>();

        await context.Database.MigrateAsync(ct);
        await seeder.SeedAsync();
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
