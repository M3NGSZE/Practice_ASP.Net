using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;

namespace SuperHeroAPI_DotNet6.Repositories.Implementations
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly DataContext _dataContext;

        public SuperHeroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<SuperHero>> GetAllAsync()
        {
            return await _dataContext.SuperHeroes.ToListAsync();
        }

        public async Task<SuperHero?> GetAsync(int id)
        {
            return await _dataContext.SuperHeroes.FindAsync(id);
        }
    }
}
