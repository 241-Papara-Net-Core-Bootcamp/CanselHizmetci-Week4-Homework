using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Services.Abstracts;
using RepositoryPattern.Services.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryPattern.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDapperController : ControllerBase
    {
        private readonly IBookDapperService _bookDapperService;

        public BookDapperController(IBookDapperService bookDapperService)
        {
            _bookDapperService = bookDapperService;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookDapperService.GetAll());
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item= _bookDapperService.Get(id);
            if (item == null) return BadRequest("Kitap Bulunamadı");
            return Ok(item);
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] BookDTO newBook)
        {
            _bookDapperService.Add(newBook);
            return Ok();
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookDTO updateBook)
        {
            _bookDapperService.Update(id,updateBook);
            return Ok();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookDapperService.Delete(id);
            return Ok();
        }
    }
}
