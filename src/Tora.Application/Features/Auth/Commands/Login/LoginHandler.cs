using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tora.Application.Common.Models;
using Tora.Application.Interfaces;

namespace Tora.Application.Features.Auth.Commands.Login;

public class LoginHandler(IToraDbContext dbContext, 
                          IJwtService jwtService,
                          IHashingService hashingService) 
                          : IRequestHandler<LoginCommand, ApiResponse<string>>
{
    private readonly IToraDbContext _dbContext = dbContext;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<ApiResponse<string>> Handle(LoginCommand request,  CancellationToken ct)
    {
        var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email, ct);
        if(user == null || !hashingService.Verify(request.Password, user.Password))
        {
            throw new BadHttpRequestException("Invalid user credentials");
        }
        if(user.Role == null)
        {
            throw new BadHttpRequestException("User role is not loaded");
        }
        return ApiResponse<string>.SuccessResponse(_jwtService.GenerateToken(user.Id.ToString(), user.Email, user.Role.UserRole.ToString()), "Logged in successfully");
    }
}
