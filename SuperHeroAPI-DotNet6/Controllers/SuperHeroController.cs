using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Reponse;
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

        /*[HttpGet("new-response")]
        public async Task<ActionResult<ApiResponse<List<SuperHeroDTO>>>> GetAllSuperHero()
        {
            return null;
        }*/

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
    }
}
