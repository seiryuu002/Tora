using Microsoft.EntityFrameworkCore;
using Tora.Persistence;

namespace Tora.Api;

public static class ToraDbMigrateExtension
{
    public static void AddToraDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ToraDbContext>(options =>
        options.UseSqlite(connectionString, b => b.MigrationsAssembly("Tora.Persistence")));
    }
}
