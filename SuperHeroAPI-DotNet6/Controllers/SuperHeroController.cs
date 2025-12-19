using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet6.Models.Entities;
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
    }
}
