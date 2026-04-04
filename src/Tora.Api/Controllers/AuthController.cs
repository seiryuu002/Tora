using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tora.Application.Auth.Commands.Register;
using Tora.Application.DTOs.Auth;
using Tora.Domain.Entities;

namespace Tora.Api.Controllers
{
    [Route("tora/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var command = new RegisterCommand
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            var userId = await _mediator.Send(command);
            //logic

            return Created("", new
            {
                message = "User registered successfully",
                userId
            });
        }
    }
}



