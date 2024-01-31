using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Services;

namespace RestWithAspNetUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var Book = _bookBusiness.FindById(id);
            if (Book == null)
            {
                return NotFound();
            }
            return Ok(Book);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Book Book)
        {
            if (Book == null)
            {
                return BadRequest(); 
            }
            return Ok(_bookBusiness.Create(Book));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book Book)
        {
            if (Book == null)
            {
                return BadRequest();
            }
            return Ok(_bookBusiness.Update(Book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
