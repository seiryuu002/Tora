using MediatR;
using Tora.Application.DTOs.Users;
using Tora.Application.Common.Models;
using Tora.Application.Interfaces.Repositories;

namespace Tora.Application.Features.Users.Queries;

public class GetUserHandler(IUserReadService userReadService): IRequestHandler<GetUserQuery, ApiResponse<PaginatedResult<UserDto>>> 
{
    public async Task<ApiResponse<PaginatedResult<UserDto>>> Handle(GetUserQuery request, CancellationToken ct)
    {
        var result = await userReadService.GetUsersAsync(request.Search, request.Role, request.Page, request.PageSize, ct);

        return ApiResponse<PaginatedResult<UserDto>>.SuccessResponse(result, "Users fetched successfully");
    }
}
