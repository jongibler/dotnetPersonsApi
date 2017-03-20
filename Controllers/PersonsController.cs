using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PersonsApi.Models;

namespace PersonsApi.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        DataContext _context;

        public PersonsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var personsProjection = _context.Persons.Select(p => new { p.Id, p.Name });
            return Ok(personsProjection);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            //todo save person
        }

    }
}
