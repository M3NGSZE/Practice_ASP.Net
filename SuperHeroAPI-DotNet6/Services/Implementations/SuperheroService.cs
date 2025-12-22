using AutoMapper;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
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

        public async Task<SuperHeroDTO> GetHeroByIdAsync(int id)
        {
            SuperHero superHero = await _superHeroRepository.GetAsync(id);

            if(superHero == null)
            {
                throw new NotFoundException("Superhero not found");
            }

            
            return _mapper.Map<SuperHeroDTO>(superHero);
        }
    }
}
