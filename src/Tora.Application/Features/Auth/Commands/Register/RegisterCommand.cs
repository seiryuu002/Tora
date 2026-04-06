using MediatR;
using Tora.Application.Common.Models;
namespace Tora.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<ApiResponse<string>>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
