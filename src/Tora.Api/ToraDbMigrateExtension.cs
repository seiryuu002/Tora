using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;
using Tora.Persistence;

namespace Tora.Api;

public static class ToraDbMigrateExtension
{
    public static async System.Threading.Tasks.Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ToraDbContext>();
        
        await dbContext.Database.MigrateAsync();
        
        if (!dbContext.Roles.Any())
        {
            dbContext.Roles.AddRange(
                new Role { UserRole = "SuperAdmin" },
                new Role { UserRole = "Admin" },
                new Role { UserRole = "User" },
                new Role { UserRole = "Guest" }
            );

            await dbContext.SaveChangesAsync();
        }
    }

    public static void AddToraDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ToraDbContext>(options =>
        options.UseSqlite(connectionString, b => b.MigrationsAssembly("Tora.Persistence")));
    }

}
