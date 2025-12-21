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
            CreateMap<SuperHero, SuperHeroDTO>();
            CreateMap<SuperHeroRequest, SuperHero>();
        }
    }
}
