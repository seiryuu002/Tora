using MediatR;
namespace Tora.Application.Auth.Commands.Register;

public class RegisterCommand : IRequest<string>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
