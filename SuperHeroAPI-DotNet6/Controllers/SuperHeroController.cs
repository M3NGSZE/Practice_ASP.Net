using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Reponse;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Services.Interfaces;

namespace SuperHeroAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperheroService _superheroService;

        public SuperHeroController(ISuperheroService superheroService)
        {
            _superheroService = superheroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            /*var heroes = new List<SuperHero>
            {
                new SuperHero
                {
                    Id = 1,
                    Name = "Spider-Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                }
            };

            return Ok(heroes);*/

            return Ok(await _superheroService.GetAllAsync());
        }

        [HttpGet("new-response-dto")]
        public async Task<ActionResult<ApiResponse<List<SuperHeroDTO>>>> GetAllSuperHeroDTO()
        {
            List<SuperHeroDTO> superHeroes = await _superheroService.GetAllHeroesAsync();

            /*return Ok(new ApiResponse<List<SuperHero>>(
                message: "All superheroes successfully fetched",    // this we need to initialize the object since it's not spring without the annotation, and ...NOTE.. the the key word (..new..) it's object
                payload: superHeroes
            ));*/

            var apiResponse = new ApiResponse<List<SuperHeroDTO>>
                (
                    message: "All superheroes successfully fetched",
                    payload: superHeroes
                );

            return Ok(apiResponse);
        }

        [HttpGet("new-response")]
        public async Task<ActionResult<ApiResponse<List<SuperHero>>>> GetAllSuperHero()
        {
            List<SuperHero> superHeroes = await _superheroService.GetAllAsync();
            var apiResponse = new ApiResponse<List<SuperHero>>
                (
                    message: "All superheroes successfully fetched",
                    statusCode: 200,
                    payload: superHeroes
                );

            return Ok(apiResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SuperHeroDTO>> GetAsyncById(int id)
        {

/*            var apiResponse = new ApiResponse<SuperHeroDTO>
                (
                    message: "All superheroes successfully fetched",
                    payload: await _superheroService.GetHeroByIdAsync(id)
                );*/


            // redude write short code
            return Ok(new ApiResponse<SuperHeroDTO>
                (
                    message: "A Superheroe successfully fetched",
                    payload: await _superheroService.GetHeroByIdAsync(id)
                ));
        }

        [HttpPost]
        public async Task<ActionResult<SuperHeroDTO>> createSuperHero(SuperHeroRequest request)
        {
            return Ok(new ApiResponse<SuperHeroDTO>
                (
                    message: "New superheroe successfully created",
                    statusCode: 201,
                    payload: await _superheroService.CreateHeroAsync(request)
                )); 
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SuperHeroDTO>> UpdateSuperHero(int id, SuperHeroRequest request)
        {
            return Ok(new ApiResponse<SuperHeroDTO>
                (
                    message: "A superheroe successfully updated",
                    statusCode: 201,
                    payload: await _superheroService.UpdateHeroAsync(id, request)
                )); 
        }
    }
}
