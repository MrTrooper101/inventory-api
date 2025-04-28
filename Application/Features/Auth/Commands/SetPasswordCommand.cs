using inventory_api.Application.Features.Auth.DTOs;
using MediatR;

namespace inventory_api.Application.Features.Auth.Commands
{
    public class SetPasswordCommand : IRequest<bool>
    {
        public SetPasswordDto SetPasswordDto;

        public SetPasswordCommand(SetPasswordDto setPasswordDto)
        {
            SetPasswordDto = setPasswordDto;
        }
    }
}
