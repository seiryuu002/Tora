using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;

namespace Tora.Infrastructure.Persistence.Seed;

public class DbSeeder(ToraDbContext context)
{
    private readonly ToraDbContext _context = context;

    public async System.Threading.Tasks.Task SeedAsync()
    {
        var roles = new[] { "SuperAdmin", "Manager", "Developer", "Guest" };

        // seed Roles = {"SuperAdmin", "Manager", "Developer", "Guest"}
        foreach ( var role in roles)
        {
            if (!await _context.Roles.AnyAsync(u => u.UserRole == role))
            {
                await _context.Roles.AddAsync(new Role { Id = Guid.NewGuid(), UserRole = role});
            }
        }

        await _context.SaveChangesAsync();

        // seed a default user with superadmin role
        var superAdminRole = await _context.Roles.FirstOrDefaultAsync(u => u.UserRole == "SuperAdmin")?? 
        throw new Exception("SuperAdmin role not found");

        var exists = await _context.Users.AnyAsync(u => u.RoleId == superAdminRole.Id);

        if(!exists)
        {
            var superAdminUser = new User
            {
                Id = Guid.NewGuid(), 
                Name = "Owner",
                Email = "superadmin@tora.com",
                RoleId = superAdminRole.Id,
                Role = superAdminRole,
                Password = BCrypt.Net.BCrypt.HashPassword("SuperAdmin@12345")
            };
            await _context.Users.AddAsync(superAdminUser);
            await _context.SaveChangesAsync();
        }
    }
}
