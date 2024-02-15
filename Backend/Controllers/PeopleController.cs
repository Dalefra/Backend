using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("people")]IPeopleService peopleService) {
            _peopleService = peopleService;
        }



        [HttpGet("all")]
        public List<People> GetPeoples() => Repository.People;

        // Antes 
        /*
            return Repository.People.First(p => p.Id == id);
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search)
        {
            return Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();
        }
        */

        // Despues
        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var result = Repository.People.FirstOrDefault(p => p.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            else { 
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult Agregar (People people)
        {
            if (!_peopleService.Validation(people))
            {
                return BadRequest();
            }
            Repository.People.Add(people);

            return NoContent();
        }
    }


    public class People
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }
    }

    // Simualacion de base de datos
    public class Repository
    {
        public static List<People> People = new List<People>
        {
          new People(){ Id = 1, Name =  "David", Birthdate = new DateTime(1998,04,3) },
          new People(){ Id = 2, Name =  "David2", Birthdate = new DateTime(1998,04,3) },
          new People(){ Id = 3, Name =  "David3", Birthdate = new DateTime(1998,04,3) },
          new People(){ Id = 4, Name =  "David4", Birthdate = new DateTime(1998,04,3) },
        };
    }


}
