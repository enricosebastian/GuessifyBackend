using Microsoft.AspNetCore.Mvc;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgifyController : Controller
    {
        private readonly IAgifyService _agifyService;
        public AgifyController(IAgifyService agifyService)
        {
            _agifyService = agifyService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string name, string countryId = "")
        {
            var response = await _agifyService.GetAgifyResult(name, countryId);
            return Ok(response);
        }

        [HttpGet("GetMany")]
        public async Task<IActionResult> Get([FromQuery] List<string> name)
        {
            var response = await _agifyService.GetAgifyResult(name);
            return Ok(response);
        }
    }
}
