namespace Tora.Application.DTOs.Auth;

public record RegisterDto
(
    string Name,
    string Email,
    string Password
);
