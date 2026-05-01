using Microsoft.EntityFrameworkCore;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Users;
using Tora.Application.Interfaces.Repositories;

namespace Tora.Infrastructure.Persistence.Repositories;

public class UserReadService(ToraDbContext dbContext) : IUserReadService
{
    public async Task<PaginatedResult<UserDto>> GetUsersAsync(string? search, string? role, int page, int pageSize, CancellationToken ct)
    {
        var query = dbContext.Users.AsNoTracking()
                                   .Include(u => u.Role)
                                   .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(u => 
                                u.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase) || 
                                u.Email.Contains(search, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(role))
        {
            query = query.Where(u => 
            u.Role != null && 
            u.Role.UserRole.Equals(role, StringComparison.CurrentCultureIgnoreCase));
        }

        var totalCount = await query.CountAsync(ct);

        var users = await query.OrderBy(u => u.Name)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .Select(u => new UserDto
                               {
                                   Name = u.Name,
                                   Email = u.Email,
                                   Role = u.Role!.UserRole
                               })
                               .ToListAsync(ct);

        return new PaginatedResult<UserDto>
        {
            Items = users,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}
