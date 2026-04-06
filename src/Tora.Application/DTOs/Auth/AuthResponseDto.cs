namespace Tora.Application.DTOs.Auth;

public record AuthResponseDto
(
    string Token,
    string Email,
    string Role
);