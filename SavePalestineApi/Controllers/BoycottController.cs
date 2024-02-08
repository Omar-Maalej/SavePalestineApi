using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavePalestineApi.Models;
using SavePalestineApi.Repositories;

namespace SavePalestineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoycottController : ControllerBase
    {
        private readonly IBoycottRepository _boycottRepository;
        public BoycottController(IBoycottRepository boycottRepository)
        {
           _boycottRepository = boycottRepository;
        }
        [HttpGet]
        public ActionResult GetBoycotts()
        {
            var boycotts = _boycottRepository.GetBoycotts();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(boycotts);
        }

        [HttpPost]
        public ActionResult AddBoycott([FromForm] Boycott boycott, IFormFile formFile)
        {
            _boycottRepository.AddBoycott(boycott, formFile);
            return Ok(boycott);
        }

        [HttpGet("{id}")]
        public ActionResult GetBoycott(int id)
        {
            var boycott = _boycottRepository.GetBoycott(id);
            if (boycott == null)
            {
                return NotFound();

            }
            return Ok(boycott);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBoycott(int id, [FromForm] Boycott boycott, IFormFile formFile)
        {
            if (id != boycott.Id)
            {
                return BadRequest();
            }

            _boycottRepository.UpdateBoycott(boycott, formFile );
            return Ok(boycott);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteBoycott(int id)
        {

            var boycott = _boycottRepository.GetBoycott(id);
            if (boycott == null)
            {
                return NotFound();
            }
            _boycottRepository.DeleteBoycott(boycott);
            return Ok(boycott);
        }
    }
}
