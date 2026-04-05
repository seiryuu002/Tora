using MediatR;

namespace Tora.Application.Auth.Commands.Login;

public class LoginCommand : IRequest<string>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
