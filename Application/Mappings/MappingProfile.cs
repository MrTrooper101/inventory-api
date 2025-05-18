using AutoMapper;
using inventory_api.Application.Features.Auth.DTOs;
using inventory_api.Application.Features.Products.DTOs;
using inventory_api.Domain.Entities;

namespace inventory_api.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserResponseDto>();

            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
