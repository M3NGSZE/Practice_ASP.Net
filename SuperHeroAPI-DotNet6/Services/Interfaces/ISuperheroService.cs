using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Services.Interfaces
{
    public interface ISuperheroService
    {
        Task<List<SuperHero>> GetAllAsync();

        Task<List<SuperHeroDTO>> GetAllHeroesAsync();

        Task<SuperHeroDTO> GetHeroByIdAsync(int id);
    }
}
