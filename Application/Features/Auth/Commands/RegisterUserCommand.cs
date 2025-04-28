using inventory_api.Application.Features.Auth.DTOs;
using MediatR;

namespace inventory_api.Application.Features.Auth.Commands
{
    public class RegisterUserCommand : IRequest<RegisterUserResponseDto>
    {
        public RegisterUserDto RegisterUserDto;

        public RegisterUserCommand(RegisterUserDto registerUserDto)
        {
            RegisterUserDto = registerUserDto;
        }
    }
}
