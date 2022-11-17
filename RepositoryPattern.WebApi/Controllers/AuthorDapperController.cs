using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Services.Abstracts;
using RepositoryPattern.Services.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryPattern.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorDapperController : ControllerBase
    {
        private readonly IAuthorDapperService _authorDapperService;

        public AuthorDapperController(IAuthorDapperService authorDapperService)
        {
            _authorDapperService = authorDapperService;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_authorDapperService.GetAll());
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _authorDapperService.Get(id);
            if (item == null)
                return BadRequest("Yazar Bulunamadı");
            return Ok(item);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthorDTO newAuthor)
        {
            _authorDapperService.Add(newAuthor);
            return Ok();
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AuthorDTO updateAuthor)
        {
            _authorDapperService.Update(id,updateAuthor);
            return Ok();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _authorDapperService.Delete(id);
            return Ok();

        }
    }
}
