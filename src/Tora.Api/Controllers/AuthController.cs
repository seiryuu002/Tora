using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tora.Application.Features.Auth.Commands.Login;
using Tora.Application.Features.Auth.Commands.Register;
using Tora.Application.DTOs.Auth;
using Tora.Application.Common.Models;
using Tora.Application.Common.Constants;

namespace Tora.Api.Controllers
{
    [Route("tora/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = Roles.SuperAdmin)]
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            return Created(string.Empty, result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}



