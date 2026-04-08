using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tora.Application.Common.Constants;
using Tora.Application.Common.Models;
using Tora.Application.DTOs.Users;
using Tora.Application.Features.Users.Queries;

namespace Tora.Api.Controllers
{
    [Route("tora/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginatedResult<UserDto>>>> GetUser([FromQuery] GetUserQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
