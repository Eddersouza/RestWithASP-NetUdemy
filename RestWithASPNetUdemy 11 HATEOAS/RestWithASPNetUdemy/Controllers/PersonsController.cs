using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;
using Tapioca.HATEOAS;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
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
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET: api/Value/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        // POST: api/Value
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(value));
        }

        // PUT: api/Value/5
        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(int id, [FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(value);

            if (updatedPerson == null) return NoContent();

            return new ObjectResult(updatedPerson);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}