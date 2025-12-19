using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Repositories.Interfaces
{
    public interface ISuperHeroRepository
    {
        Task<List<SuperHero>> GetAllAsync();
    }
}
