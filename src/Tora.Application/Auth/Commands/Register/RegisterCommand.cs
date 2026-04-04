using MediatR;
namespace Tora.Application.Auth.Commands.Register;

public class RegisterCommand : IRequest<string>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
