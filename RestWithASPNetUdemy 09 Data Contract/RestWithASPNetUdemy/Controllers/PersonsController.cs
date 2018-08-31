using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;

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
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        // POST: api/Value
        [HttpPost]
        public IActionResult Post([FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(value));
        }

        // PUT: api/Value/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(value);

            if (updatedPerson == null) return NoContent();

            return new ObjectResult(updatedPerson);
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