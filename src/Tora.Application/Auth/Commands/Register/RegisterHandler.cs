using MediatR;
using Microsoft.EntityFrameworkCore;
using Tora.Persistence;
namespace Tora.Application.Auth.Commands.Register;

public class RegisterHandler(ToraDbContext context) : IRequestHandler<RegisterCommand, string>
{
    private readonly ToraDbContext _context = context;

    public async Task<string> Handle(RegisterCommand request, CancellationToken ct)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == request.Email, ct);
        if (exists)
        {
            throw new Exception("User with this email already exists");
        }

        var role = await _context.Roles
        .FirstOrDefaultAsync(r => r.UserRole == "User", ct) ?? throw new Exception("Default user role not found");
        
        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            RoleId = role.Id,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);

        return user.Id.ToString();
    }

}
