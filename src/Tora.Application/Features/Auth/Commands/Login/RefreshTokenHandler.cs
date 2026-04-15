using MediatR;
using Microsoft.EntityFrameworkCore;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Auth;
using Tora.Application.Interfaces;
using Tora.Domain.Entities;

namespace Tora.Application.Features.Auth.Commands.Login;

public class RefreshTokenHandler(IToraDbContext dbContext, 
                                 IJwtService jwtService,
                                 IHashingService hashingService) : 
                                 IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var tokens = await dbContext.RefreshTokens
                                   .Include(u=> u.User)
                                   .ThenInclude(u => u.Role)
                                   .Where(r  => !r.IsRevoked && r.ExpiryDate > DateTime.UtcNow).ToListAsync(ct);

        var token = tokens.FirstOrDefault(r => hashingService.Verify(request.RefreshToken, r.Token)) ?? 
                           throw new Exception("Refresh Token expired");
        token.IsRevoked = true;

        var newAccessToken = jwtService.GenerateAccessToken(token.User.Name, token.User.Email, token.User.Role!.UserRole);
        var unhashedRefreshToken = jwtService.GenerateRefreshToken();
        var newRefreshToken = hashingService.Hash(unhashedRefreshToken);

        var newRefreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = newRefreshToken,
            UserId = token.User.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };

        dbContext.RefreshTokens.Add(newRefreshTokenEntity);
        await dbContext.SaveChangesAsync(ct);

        return ApiResponse<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = unhashedRefreshToken,
            Email = token.User.Email,
            Role = token.User.Role!.UserRole
        });
    }
}
