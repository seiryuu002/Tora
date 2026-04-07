using MediatR;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Auth;

namespace Tora.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<ApiResponse<AuthResponseDto>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
