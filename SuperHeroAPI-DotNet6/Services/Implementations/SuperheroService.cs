using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Interfaces;

namespace SuperHeroAPI_DotNet6.Services.Implementations
{
    public class SuperheroService : ISuperheroService
    {
        private readonly ISuperHeroRepository _superHeroRepository;

        public SuperheroService (ISuperHeroRepository superHeroRepository)
        {
            _superHeroRepository = superHeroRepository;
        }

        public async Task<List<SuperHero>> GetAllAsync()
        {
            return await _superHeroRepository.GetAllAsync();
        }
    }
}
