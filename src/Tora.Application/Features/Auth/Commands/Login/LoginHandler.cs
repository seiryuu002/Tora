using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Auth;
using Tora.Application.Interfaces;
using Tora.Domain.Entities;
using Tora.Domain.Exceptions;

namespace Tora.Application.Features.Auth.Commands.Login;

public class LoginHandler(IToraDbContext dbContext, 
                          IJwtService jwtService,
                          IHashingService hashingService,
                          ILogger<LoginHandler> logger) 
                          : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand request,  CancellationToken ct)
    {
        var user = await dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email, ct);

        logger.LogInformation("Attempting to login");
        
        if(user == null || !hashingService.Verify(request.Password, user.Password))
        {
            logger.LogWarning("Login failed");
            throw new NotFoundException("Invalid user credentials");
        }
        if(user.Role == null)
        {
            throw new Exception("User role is not loaded");
        }

        var newAccessToken = jwtService.GenerateAccessToken(user.Name, 
                                                            user.Email, user.Role.UserRole);
        var newRefreshToken = jwtService.GenerateRefreshToken();
        var newRefreshTokenEntity = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            Token = hashingService.Hash(newRefreshToken),
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            User = user
        };

        dbContext.RefreshTokens.Add(newRefreshTokenEntity);
        await dbContext.SaveChangesAsync(ct);

        var authResponse = new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            Email = user.Email,
            Role = user.Role.UserRole
        };
        return ApiResponse<AuthResponseDto>.SuccessResponse(authResponse, "Logged in successfully");
    }
}
