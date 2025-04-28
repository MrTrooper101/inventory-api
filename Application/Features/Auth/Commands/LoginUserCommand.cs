using inventory_api.Application.Features.Auth.DTOs;
using MediatR;

namespace inventory_api.Application.Features.Auth.Commands
{
    public class LoginUserCommand : IRequest<LoginUserResponseDto>
    {
        public LoginUserDto LoginUserDto { get; set; }

        public LoginUserCommand(LoginUserDto loginUserDto)
        {
            LoginUserDto = loginUserDto;
        }
    }
}
