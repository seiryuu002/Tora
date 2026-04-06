using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tora.Infrastructure;
using Tora.Infrastructure.Interfaces;

namespace Tora.Application.Auth.Commands.Login;

public class LoginHandler(ToraDbContext dbContext, IJwtService jwtService) : IRequestHandler<LoginCommand, string>
{
    private readonly ToraDbContext _dbContext = dbContext;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<string> Handle(LoginCommand request,  CancellationToken ct)
    {
        var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email, ct);
        
        if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new Exception("Invalid user credentials");
        }
        if(user.Role == null)
        {
            throw new Exception("User role is not loaded");
        }
        return _jwtService.GenerateToken(user.Id.ToString(), user.Email, user.Role.UserRole.ToString());
    }
}
