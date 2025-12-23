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

        public async Task<SuperHero> CreateAsync(SuperHero entity)
        {
            _dataContext.SuperHeroes.Add(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(SuperHero entity)
        {
            _dataContext?.SuperHeroes.Remove(entity); // mark as Deleted
            return Task.CompletedTask;
        }

        public async Task<List<SuperHero>> GetAllAsync()
        {
            return await _dataContext.SuperHeroes.ToListAsync();
        }

        public async Task<SuperHero?> GetAsync(int id)
        {
            return await _dataContext.SuperHeroes.FindAsync(id);
        }

        public IQueryable<SuperHero> QueryableAsync()
        {
            return _dataContext.SuperHeroes.AsQueryable();
        }

        /*public IQueryable<SuperHero>> QueryableAsync()
        {
            IQueryable<SuperHero> superHeroes = _dataContext.SuperHeroes.AsQueryable();
            return null;
        }*/

        public async Task UpdateAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
