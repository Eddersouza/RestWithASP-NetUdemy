using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Services;

namespace RestWithASPNetUdemy.Controllers
{
    [Produces("application/json")]
    [Route("api/persons")]
    public class PersonsController : Controller
    {
        IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/Value
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET: api/Value/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        // POST: api/Value
        [HttpPost]
        public IActionResult Post([FromBody]Person value)
        {
            if (value == null) return BadRequest();
            return new ObjectResult(_personService.Create(value));
        }

        // PUT: api/Value/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person value)
        {
            if (value == null) return BadRequest();
            return new ObjectResult(_personService.Update(value));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}