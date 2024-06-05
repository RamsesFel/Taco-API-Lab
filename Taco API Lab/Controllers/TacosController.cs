using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taco_API_Lab.Model;

namespace Taco_API_Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult AllTacos(bool? softshell = null)
        {
            List<Taco> results = dbContext.Tacos.ToList();
            if (softshell == true)
            {
                results = results.Where(s=> s.SoftShell == true).ToList();
            }
            return Ok(results);
        }
        [HttpGet("{id}")]
        public IActionResult GetTacos(int id)
        {
            Taco result = dbContext.Tacos.Find(id);
            if (result == null) { return NotFound("Taco not found"); }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddTaco([FromBody] Taco newTaco)
        {
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();
            return Created($"api/Taco/{newTaco.Id}", newTaco);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTaco(int id)
        {
            Taco result = dbContext.Tacos.Find(id);
            if(result == null) { return NotFound(); }
            dbContext.Tacos.Remove(result);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
