using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using Taco_API_Lab.Model;

namespace Taco_API_Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult AllDrinks(string? SortByCost = null)
        {
            List<Drink> results = dbContext.Drinks.ToList();
            if (SortByCost.ToLower().Trim() == "ascending")
            {
                results = results.OrderBy(i => i.Cost).ToList();
            }
            else if (SortByCost.ToLower().Trim() == "descending")
            {
                results = results.OrderByDescending(i => i.Cost).ToList();
            }
            return Ok(results);
        }
        [HttpGet("{id}")]
        public IActionResult GetDrinks(int id)
        {
            Drink result = dbContext.Drinks.Find(id);
            if (result == null) { return NotFound("Drink not found"); }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddDrink([FromBody] Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            return Created($"api/Taco/{newDrink.Id}", newDrink);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateDrink([FromBody] Drink targetDrink, int id)
        {
            if (id != targetDrink.Id) { return BadRequest(); }
            if (!dbContext.Drinks.Any(b => b.Id == id)) { return NotFound(); }

            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
