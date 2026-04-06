using MediatR;
using Tora.Application.Common.Models;

namespace Tora.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<ApiResponse<string>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
