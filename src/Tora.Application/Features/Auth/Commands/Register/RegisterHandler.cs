using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Auth;
using Tora.Application.Interfaces;
namespace Tora.Application.Features.Auth.Commands.Register;

public class RegisterHandler(IToraDbContext context, 
                             IHashingService hashingService, 
                             ILogger<RegisterHandler> logger) : 
                             IRequestHandler<RegisterCommand, ApiResponse<string>>
{
    private readonly IToraDbContext _context = context;

    public async Task<ApiResponse<string>> Handle(RegisterCommand request, CancellationToken ct)
    {
        logger.LogInformation("Attempting to register the user");

        var exists = await _context.Users.AnyAsync(u => u.Email == request.Email, ct);
        if (exists)
        {
            logger.LogWarning("Registeration failed - email already exists {email}", request.Email);
            throw new Exception("User with this email already exists");
        }

        var role = await _context.Roles
        .FirstOrDefaultAsync(r => r.UserRole == "Guest", ct) ?? throw new Exception("Default user role not found");
        
        var hashedPassword = hashingService.Hash(request.Password);
        
        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            RoleId = role.Id,
            Role = role,
            Email = request.Email,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);

        return ApiResponse<string>.SuccessResponse("User Created");
    }

}
