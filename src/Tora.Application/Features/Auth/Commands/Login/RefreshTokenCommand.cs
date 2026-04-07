using MediatR;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Auth;

namespace Tora.Application.Features.Auth.Commands.Login;

public class RefreshTokenCommand : IRequest<ApiResponse<AuthResponseDto>> 
{    
    public string RefreshToken {get; set;} = string.Empty;
}
