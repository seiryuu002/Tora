using MediatR;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Users;

namespace Tora.Application.Features.Users.Queries;

public class GetUserQuery : IRequest<ApiResponse<PaginatedResult<UserDto>>>
{
    public int Page {get; set;} = 1;
    public int PageSize {get; set;} = 10;

    //For filtering
    public string? Search { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }

}
