using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Users;
using Tora.Application.Interfaces;

namespace Tora.Application.Features.Users.Queries;

public class GetUserHandler(IToraDbContext dbContext): IRequestHandler<GetUserQuery, ApiResponse<PaginatedResult<UserDto>>> 
{
    public async Task<ApiResponse<PaginatedResult<UserDto>>> Handle(GetUserQuery request, CancellationToken ct)
    {
        var query = dbContext.Users.Include(u => u.Role).AsQueryable();

        // filtering
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(u => 
            u.Name.Contains(search) ||
            u.Email.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(request.Role))
        {
            query = query.Where(u => u.Role!.UserRole == request.Role);
        }

        var totalCount = await query.CountAsync(ct);

        //Pagination
        var users = await query.OrderBy(u => u.Name)
                               .Skip((request.Page - 1) * request.PageSize)
                               .Take(request.PageSize)
                               .Select(u => new UserDto
                               {
                                   Name = u.Name,
                                   Email = u.Email,
                                   Role = u.Role!.UserRole
                               })
                               .ToListAsync(ct);

        var paginatedResult = new PaginatedResult<UserDto>
        {
            Items = users,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
        return ApiResponse<PaginatedResult<UserDto>>.SuccessResponse(paginatedResult, "Users fetched successfully");
    }
}
