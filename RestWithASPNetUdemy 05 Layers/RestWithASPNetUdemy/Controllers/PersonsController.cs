using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Business;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
    [Produces("application/json")]
    [Route("api/persons/v{version:apiVersion}")]
    public class PersonsController : Controller
    {
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        // GET: api/Value
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET: api/Value/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        // POST: api/Value
        [HttpPost]
        public IActionResult Post([FromBody]Person value)
        {
            if (value == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(value));
        }

        // PUT: api/Value/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person value)
        {
            if (value == null) return BadRequest();
            return new ObjectResult(_personBusiness.Update(value));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}