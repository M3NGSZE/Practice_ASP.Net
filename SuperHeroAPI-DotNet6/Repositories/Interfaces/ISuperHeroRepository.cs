using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Repositories.Interfaces
{
    public interface ISuperHeroRepository
    {
        Task<List<SuperHero>> GetAllAsync();

        Task<SuperHero> GetAsync(int id);

        Task<SuperHero> CreateAsync(SuperHero entity);
    }
}
