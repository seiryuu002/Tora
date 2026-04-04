using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tora.Application.Auth.Commands.Register;
using Tora.Application.DTOs.Auth;

namespace Tora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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



