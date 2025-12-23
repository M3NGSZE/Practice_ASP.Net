using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Reponses;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Interfaces;

namespace SuperHeroAPI_DotNet6.Services.Implementations
{
    public class SuperheroService : ISuperheroService
    {
        private readonly ISuperHeroRepository _superHeroRepository;
        private readonly IMapper _mapper;

        public SuperheroService (ISuperHeroRepository superHeroRepository, IMapper mapper)
        {
            _superHeroRepository = superHeroRepository;
            _mapper = mapper;
        }

        public async Task<SuperHeroDTO> CreateHeroAsync(SuperHeroRequest superHeroRequest)
        {
            SuperHero superHero = _mapper.Map<SuperHero>(superHeroRequest);
            var savedUser = await _superHeroRepository.CreateAsync(superHero);
            return _mapper.Map<SuperHeroDTO>(savedUser); ;
        }

        public async Task DeleteHeroByIdAsync(int id)
        {
            var sup = await GetHeroEntity(id);

            // entity is tracked
            await _superHeroRepository.DeleteAsync(sup);

            await _superHeroRepository.UpdateAsync();

        }

        public async Task<List<SuperHero>> GetAllAsync()
        {
            return await _superHeroRepository.GetAllAsync();
        }

        public async Task<List<SuperHeroDTO>> GetAllHeroesAsync()
        {
            /*             var superHeroes = await _superHeroRepository.GetAllAsync();
                        return _mapper.Map<List<SuperHeroDTO>>(superHeroes);*/


            // return _mapper.Map<List<SuperHeroDTO>>(superHeroes);
            /*            return superHeroes
                    .Select(hero => _mapper.Map<SuperHeroDTO>(hero))
                    .ToList();*/

            return (await _superHeroRepository.GetAllAsync())
                .Select(_mapper.Map<SuperHeroDTO>)
                .ToList();
        }

        public async Task<ListResponse<SuperHeroDTO>> GetHeroaPaginationAsync(int page, int size, string? name)
        {
            /*var query = _superHeroRepository.QueryableAsync();

            var totalElements = await query.CountAsync();          // total records
            var totalPages = (int)Math.Ceiling(totalElements / (double)size);  // total pages

            *//*            var heroes = await query
                            .OrderBy(h => h.Id)                               // ensure consistent order
                            .Skip((page - 1) * size)
                            .Take(size)
                            .ToListAsync();*//*

            var heroes = await query
                .OrderBy(h => h.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(h => new SuperHeroDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    FirstName = h.FirstName,
                    LastName = h.LastName,
                 })
                .ToListAsync();


            return new ListResponse<SuperHeroDTO>
            {
                Elements = heroes,
                PaginationResponse = PaginationResponse.CalculatePagination(totalElements, page, size)
            };*/

            var query = _superHeroRepository.QueryableAsync();

            // 🔍 SEARCH (optional)
            if (!string.IsNullOrWhiteSpace(name))
            {
                /*query = query.Where(h => h.Name.Contains(name));*/
                query = query.Where(h => h.Name.ToLower().Contains(name.ToLower()));
                // PostgreSQL (case-insensitive alternative):
                // query = query.Where(h => EF.Functions.ILike(h.Name, $"%{name}%"));
            }

            var totalElements = await query.CountAsync();

            var heroes = await query
                .OrderBy(h => h.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(h => new SuperHeroDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    FirstName = h.FirstName,
                    LastName = h.LastName,
                })
                .ToListAsync();

            return new ListResponse<SuperHeroDTO>
            {
                Elements = heroes,
                PaginationResponse = PaginationResponse.CalculatePagination(totalElements, page, size)
            };
        }

        public async Task<SuperHeroDTO> GetHeroByIdAsync(int id)
        {
            /*var superHero = await _superHeroRepository.GetAsync(id);

            if(superHero == null)
                throw new NotFoundException("Superhero not found");*/

            var superHero = await GetHeroEntity(id);

            return _mapper.Map<SuperHeroDTO>(superHero);
        }

        public async Task<SuperHero> GetHeroEntity(int id)
        {
            var superHero = await _superHeroRepository.GetAsync(id);

            if (superHero == null)
                throw new NotFoundException("Superhero not found");

            return superHero;
        }

        public async Task<SuperHeroDTO> UpdateHeroAsync(int id, SuperHeroRequest superHeroRequest)
        {
            /*var superHero = await GetHeroByIdAsync(id);

            SuperHero sup = _mapper.Map<SuperHero>(superHero);

            Console.WriteLine(sup);*/

            /*var sup = await _superHeroRepository.GetAsync(id);

            if (sup == null)
                throw new NotFoundException("Superhero not found");*/

            // found new solution (first create method that return entity then have two seperate method one map to dto and one keep origin entity // we can use it later)
            var sup = await GetHeroEntity(id);

            sup.Name = superHeroRequest.Name;
            sup.FirstName = superHeroRequest.FirstName;
            sup.LastName = superHeroRequest.LastName;
            sup.Place = superHeroRequest.Place;

            await _superHeroRepository.UpdateAsync();

            return _mapper.Map<SuperHeroDTO>(sup);
        }
    }
}
