using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tora.Application.Interfaces;
using Tora.Domain.Entities;
using Tora.Domain.Exceptions;

namespace Tora.Infrastructure.Persistence.Seed;

public class DbSeeder(IToraDbContext context, IHashingService hashingService, IConfiguration config)
{
    public async System.Threading.Tasks.Task SeedAsync(CancellationToken ct)
    {
        var roles = new[] { "SuperAdmin", "Manager", "Developer", "Guest" };

        // seed Roles = {"SuperAdmin", "Manager", "Developer", "Guest"}
        foreach ( var role in roles)
        {
            if (!await context.Roles.AnyAsync(u => u.UserRole == role, ct))
            {
                await context.Roles.AddAsync(new Role { Id = Guid.NewGuid(), UserRole = role}, ct);
            }
        }

        await context.SaveChangesAsync(ct);

        // seed a default user with superadmin role
        var superAdminRole = await context.Roles.FirstOrDefaultAsync(u => u.UserRole == "SuperAdmin", ct)?? 
        throw new InvalidOperationException("SuperAdmin role not found after seeding");

        var superAdminExists = await context.Users.AnyAsync(u => u.RoleId == superAdminRole.Id, ct);

        if(!superAdminExists)
        {
            var email = config["SuperAdmin:Email"] ?? throw new InvalidOperationException("SuperAdmin email not found in configuration");
            var password = config["SuperAdmin:Password"] ?? throw new InvalidOperationException("SuperAdmin password not found in configuration");
            
            var superAdmin = User.Create(
                name: "Owner", 
                email: email,
                hashedPassword: hashingService.Hash(password),
                roleId: superAdminRole.Id);
            
            await context.Users.AddAsync(superAdmin, ct);
            await context.SaveChangesAsync(ct);
        }
    }
}
