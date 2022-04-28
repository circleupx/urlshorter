using Microsoft.AspNetCore.Mvc;

namespace urlshorter.Controllers
{
    [ApiController]
    [Route("api")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UrlController : ControllerBase
    {
        [HttpGet]
        [Route("urls")]
        [ProducesResponseType(typeof(IEnumerable<UrlEntity>), StatusCodes.Status200OK)]
        public IActionResult GetUrlResourceCollection()
        {
            return Ok();
        }

        [HttpGet]
        [Route("urls/{id}")]
        [ProducesResponseType(typeof(UrlEntity), StatusCodes.Status200OK)]
        public IActionResult GetUrlResource(int id) 
        {
            return Ok();
        }

        [HttpPost]
        [Route("urls")]
        [ProducesResponseType(typeof(UrlEntity), StatusCodes.Status201Created)]
        public IActionResult CreateUrlResource(string url)
        {
            return Created("", new { });
        }
    }
}
