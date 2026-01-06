using AutoMapper;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            // first param is value that pass in and second param is value that map to ..(Ex).. SuperHero to SuperHeroDTO
            CreateMap<SuperHero, SuperHeroDTO>();

            CreateMap<SuperHeroRequest, SuperHero>();

            CreateMap<SuperHeroDTO, SuperHero>();

            CreateMap<User, UserDTO>()
                .ForMember(
                    dest => dest.roles,
                    opt => opt.MapFrom(src => src.Roles.Select(r => r.Name))
            );

            CreateMap<User, AuthDTO>();

            CreateMap<UserRequest, User>();
        }
    }
}
