using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;
using Tora.Persistence;

namespace Tora.Infrastructure.Persistence.Seed;

public class DbSeeder(ToraDbContext context)
{
    private readonly ToraDbContext _context = context;

    public async System.Threading.Tasks.Task SeedAsync()
    {
        // seed Roles = {superadmin, admin, user, guest}
        if (!await _context.Roles.AnyAsync())
        {
            await _context.Roles.AddRangeAsync(
                new Role { UserRole = "SuperAdmin"},
                new Role { UserRole = "Manager"},
                new Role {UserRole = "Developer"},
                new Role { UserRole = "Guest"}
            );
        }
        
        await _context.SaveChangesAsync();

        // seed a default user with superadmin role
        var superAdminRoleGuid = await _context.Roles.Where(u => u.UserRole == "SuperAdmin").Select(u => u.Id).FirstAsync();

        if(! await _context.Users.AnyAsync(u => u.RoleId == superAdminRoleGuid))
        {
            var SuperAdminUser = new User
            {
                Id = Guid.NewGuid(), 
                Name = "Owner",
                Email = "superadmin@tora.com",
                RoleId = superAdminRoleGuid,
                Password = BCrypt.Net.BCrypt.HashPassword("SuperAdmin@12345")
            };
            await _context.Users.AddAsync(SuperAdminUser);
            await _context.SaveChangesAsync();
        }
    }
}
