using inventory_api.Application.Features.Auth.Commands;
using inventory_api.Application.Features.Auth.DTOs;
using inventory_api.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var command = new RegisterUserCommand(registerUserDto);

            var result = await _mediator.Send(command);

            var response = new ApiResponse<RegisterUserResponseDto>(
                result,
                message: "User registered successfully",
                statusCode: ApiStatusCode.Ok
            );

            return Ok(response);
        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto setPasswordDto)
        {
            var command = new SetPasswordCommand(setPasswordDto);

            var result = await _mediator.Send(command);

            var response = new ApiResponse<bool>(
                result,
                message: "Password has been set successfully.",
                statusCode: ApiStatusCode.Ok
            );

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var command = new LoginUserCommand(loginUserDto);

            var result = await _mediator.Send(command);

            var response = new ApiResponse<LoginUserResponseDto>(
                result,
                message: "Login successful",
                statusCode: ApiStatusCode.Ok
            );

            return Ok(response);
        }
    }
}
