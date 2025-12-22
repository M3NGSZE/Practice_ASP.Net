using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Services.Interfaces
{
    public interface ISuperheroService
    {
        Task<List<SuperHero>> GetAllAsync();

        Task<List<SuperHeroDTO>> GetAllHeroesAsync();

        Task<SuperHeroDTO> GetHeroByIdAsync(int id);

        Task<SuperHeroDTO> CreateHeroAsync(SuperHeroRequest superHeroRequest);

        Task<SuperHeroDTO> UpdateHeroAsync(int id, SuperHeroRequest superHeroRequest);
    }
}
