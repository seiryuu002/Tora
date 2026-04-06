namespace Tora.Application.DTOs.Auth;

public record class AuthUserDto
(
    Guid Id,
    string Name,
    string Role
);