using Microsoft.AspNetCore.Mvc;
using names.backend.Services;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenderizeController : Controller
    {
        private readonly IGenderizeService _genderizeService;
        public GenderizeController(IGenderizeService genderizeService)
        {
            _genderizeService = genderizeService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string name, string countryId = "")
        {
            var response = await _genderizeService.GetGenderizeResult(name, countryId);
            return Ok(response);
        }

        [HttpGet("GetMany")]
        public async Task<IActionResult> Get([FromQuery] List<string> name)
        {
            var response = await _genderizeService.GetGenderizeResult(name);
            return Ok(response);
        }
    }
}
