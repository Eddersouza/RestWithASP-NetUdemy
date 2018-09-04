using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
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

        /// <summary>
        /// Get persons in system.
        /// </summary>       
        /// <returns>Persons items in system.</returns>
        [HttpGet("")]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>), Description ="Get Person list successfully.")]
        [SwaggerResponse((500), Description ="System Error.")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET: api/Value/5
        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO), Description = "Get Person successfully.")]
        [SwaggerResponse((500), Description = "System Error.")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        // POST: api/Value
        [HttpPost]
        [SwaggerResponse((201), Type = typeof(PersonVO), Description = "Create Person successfully.")]
        [SwaggerResponse((400), Description = "Person data invalid.")]
        [SwaggerResponse((500), Description = "System Error.")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            return new CreatedResult("teste", _personBusiness.Create(value));
        }

        // PUT: api/Value/5
        [SwaggerResponse((200), Type = typeof(PersonVO), Description = "Updated Person successfully.")]
        [SwaggerResponse((400), Description = "Person data invalid.")]
        [SwaggerResponse((500), Description = "System Error.")]
        [HttpPut("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(int id, [FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(value);

            if (updatedPerson == null) return BadRequest();

            return new ObjectResult(updatedPerson);
        }

        // usado quando preciso apenas modificar parte pequena da entidade 
        // por exemplo alterar apenas um campo(marcar como ativa ou inativa)
        // um num universo com varios campos alterar apenas uma parte deles
        [SwaggerResponse((200), Type = typeof(PersonVO), Description = "Updated Person successfully.")]
        [SwaggerResponse((400), Description = "Person data invalid.")]
        [SwaggerResponse((500), Description = "System Error.")]
        [HttpPatch("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(int id, [FromBody]PersonVO value)
        {
            if (value == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(value);

            if (updatedPerson == null) return BadRequest();

            return new ObjectResult(updatedPerson);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [SwaggerResponse((204), Description = "Deleted Person successfully.")]
        [SwaggerResponse((400), Description = "Person data invalid.")]
        [SwaggerResponse((500), Description = "System Error.")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Delete(long id)
        {
            if (id.Equals(0)) return BadRequest();

            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}