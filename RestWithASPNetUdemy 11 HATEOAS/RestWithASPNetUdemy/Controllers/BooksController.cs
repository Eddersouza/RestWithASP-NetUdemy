using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/books/v{version:apiVersion}")]
    public class BooksController : Controller
    {
        private IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        // GET: api/Value
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        // GET: api/Value/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);

            if (book == null) return NotFound();

            return Ok(book);
        }

        // POST: api/Value
        [HttpPost]
        public IActionResult Post([FromBody]BookVO book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_bookBusiness.Create(book));
        }

        // PUT: api/Value/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BookVO book)
        {
            if (book == null) return BadRequest();
            var updatedBook = _bookBusiness.Update(book);

            if (updatedBook == null) return NoContent();

            return new ObjectResult(updatedBook);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}