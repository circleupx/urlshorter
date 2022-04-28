using Microsoft.AspNetCore.Mvc;
using urlshorter.Interfaces;

namespace urlshorter.Controllers
{
    [ApiController]
    [Route("api")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UrlController : ControllerBase
    {
        private readonly IHashingService _hashingService;
        private readonly UrlshorterContext _urlshorterContext;
        public UrlController(IHashingService hashingService ,UrlshorterContext urlshorterContext)
        {
            _hashingService = hashingService;
            _urlshorterContext = urlshorterContext;
        }

        [HttpGet]
        [Route("urls")]
        [ProducesResponseType(typeof(IEnumerable<UrlEntity>), StatusCodes.Status200OK)]
        public IActionResult GetUrlResourceCollection()
        {
            var urlResourceCollection = _urlshorterContext.Urls.ToList();
            return Ok(urlResourceCollection);
        }

        [HttpGet]
        [Route("urls/{hash}")]
        [ProducesResponseType(typeof(UrlEntity), StatusCodes.Status200OK)]
        public IActionResult GetUrlResource(string hash)
        {
            var urlResource = _urlshorterContext.Urls.FirstOrDefault(x => x.Hash == hash);
            return Ok(urlResource);
        }

        [HttpPost]
        [Route("urls")]
        [ProducesResponseType(typeof(UrlEntity), StatusCodes.Status201Created)]
        public IActionResult CreateUrlResource(string url)
        {
            var urlEntity = new UrlEntity
            {
                Url = url
            };

            _urlshorterContext.Urls.Add(urlEntity);
            _urlshorterContext.SaveChanges();

            urlEntity.Hash = _hashingService.ToBase62(urlEntity.Id);
           
            _urlshorterContext.SaveChanges();

            return CreatedAtAction(nameof(GetUrlResource), new { hash = urlEntity.Hash}, urlEntity);
        }
    }
}
