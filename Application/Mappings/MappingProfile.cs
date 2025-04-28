using AutoMapper;
using inventory_api.Application.Features.Auth.DTOs;
using inventory_api.Domain.Entities;

namespace inventory_api.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserResponseDto>();
        }
    }
}
