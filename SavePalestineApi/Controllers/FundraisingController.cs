using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavePalestineApi.Models;
using SavePalestineApi.Repositories;

namespace SavePalestineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundraisingController : ControllerBase
    {
        private readonly IFundraisingRepository _fundraisingRepository;
        public FundraisingController(IFundraisingRepository fundraisingRepository)
        {
            _fundraisingRepository = fundraisingRepository;
        }
        [HttpGet]
        public ActionResult GetFundraisings()
        {
            var fundraisings = _fundraisingRepository.GetFundraisings();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(fundraisings);
        }

        [HttpPost]
        public ActionResult AddFundraising([FromForm] Fundraising fundraising, IFormFile formFile)
        {
            _fundraisingRepository.AddFundraising(fundraising, formFile);
            return Ok(fundraising);
        }

        [HttpGet("{id}")]
        public ActionResult GetFundraising(int id)
        {
            var fundraising = _fundraisingRepository.GetFundraising(id);
            if (fundraising == null)
            {
                return NotFound();

            }
            return Ok(fundraising);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFundraising(int id, [FromForm] Fundraising fundraising)
        {
            if (id != fundraising.Id)
            {
                return BadRequest();
            }

            _fundraisingRepository.UpdateFundraising(fundraising);
            return Ok(fundraising);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteFundraising(int id)
        {

            var fundraising = _fundraisingRepository.GetFundraising(id);
            if (fundraising == null)
            {
                return NotFound();
            }
            _fundraisingRepository.DeleteFundraising(fundraising);
            return Ok(fundraising);
        }
    }
}
