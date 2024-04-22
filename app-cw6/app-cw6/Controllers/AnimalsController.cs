using app_cw6.Models;
using app_cw6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace app_cw6.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AnimalsController : ControllerBase
{
    private IAnimalsService _animalsService;

    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }

    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(string orderBy="Name")
    {
        var acceptedQueries = new List<string>() {"Name", "Description", "Category", "Area"};

        if (!acceptedQueries.Contains(orderBy))
        {
            return NotFound("Bad query");
        }
        
        var animals = _animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost] public IActionResult CreateAnimal(Animal animal)
    {
        var affectedCount = _animalsService.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{idAnimal:int}")]
    public IActionResult UpdateAnimal(int idAnimal, Animal animal)
    {
        var affectedCount = _animalsService.UpdateAnimal(idAnimal, animal);
        return StatusCode(StatusCodes.Status201Created); 
    }

    [HttpDelete("{idAnimal:int}")]
    public IActionResult DeleteAnimal(int idAnimal) 
    {
        var affectedCount = _animalsService.DeleteAnimal(idAnimal);
        return NoContent();
    }
}
