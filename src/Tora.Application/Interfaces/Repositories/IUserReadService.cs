using Tora.Application.Common.Models;
using Tora.Application.DTOs.Users;

namespace Tora.Application.Interfaces.Repositories;

public interface IUserReadService
{
    Task<PaginatedResult<UserDto>> GetUsersAsync(string? search, string? role, int page, int pageSize, CancellationToken ct);

}
