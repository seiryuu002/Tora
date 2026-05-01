using MediatR;
using Microsoft.EntityFrameworkCore;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Auth;
using Tora.Application.Interfaces;
using Tora.Domain.Entities;
using Tora.Domain.Exceptions;

namespace Tora.Application.Features.Auth.Commands.Login;

public class RefreshTokenHandler(IToraDbContext dbContext, 
                                 IJwtService jwtService,
                                 IHashingService hashingService) : 
                                 IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var prefix = request.RefreshToken.Length >= 8 
            ? request.RefreshToken[..8] 
            : throw new UnauthorizedException("Invalid refresh token");
              
        var Candidates = await dbContext.RefreshTokens
                                   .Include(u=> u.User)
                                   .ThenInclude(u => u.Role)
                                   .Where(r  => r.TokenPrefix == prefix 
                                            && !r.IsRevoked 
                                            && r.ExpiryDate > DateTime.UtcNow).ToListAsync(ct);

        var tokenEntity = Candidates.FirstOrDefault(r => hashingService.Verify(request.RefreshToken, r.Token)) ?? 
                           throw new UnauthorizedException("Refresh token is invalid, expired or has been revoked.");
        
        tokenEntity.Revoke();

        var newRefreshToken = jwtService.GenerateRefreshToken();
        var newHashedRefreshToken = hashingService.Hash(newRefreshToken);
        var newRefreshTokenEntity = RefreshToken.Create(
            rawToken: newRefreshToken,
            hashedToken: newHashedRefreshToken,
            user: tokenEntity.User
        );

        var newAccessToken = jwtService.GenerateAccessToken(
            tokenEntity.User.Id.ToString(),
            tokenEntity.User.Name,
            tokenEntity.User.Email,
            tokenEntity.User.Role!.UserRole 
        );

        dbContext.RefreshTokens.Add(newRefreshTokenEntity);
        await dbContext.SaveChangesAsync(ct);

        return ApiResponse<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            Email = tokenEntity.User.Email,
            Role = tokenEntity.User.Role!.UserRole
        });
    }
}
